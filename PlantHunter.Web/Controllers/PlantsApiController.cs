using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantHunter.Mobile.Web.Data.Models;
using PlantHunter.Mobile.Web.Models;
using web.Data;

namespace PlantHunter.Mobile.Web.Controllers
{
    [Route("api/plants")]
    [ApiController]
    public class PlantsApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public PlantsApiController(ApplicationDbContext context,
             IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: api/PlantsApi
        [HttpGet]
        public IEnumerable<Plant> GetPlants()
        {
            return _context.Plants;
        }

        // GET: api/PlantsApi/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlant([FromRoute] Guid id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var plant = await _context.Plants.FindAsync(id);

            if (plant == null)
            {
                return NotFound();
            }

            return Ok(plant);
        }

        // PUT: api/PlantsApi/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlant([FromRoute] Guid id, [FromBody] PlantAddViewModel plantViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var plant = _context.Plants.Find(id);
            //Update Properties
            plant.Longitude = plantViewModel.Longitude;
            plant.Latitude = plantViewModel.Latitude;


            _context.Entry(plant).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlantExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/PlantsApi
        [HttpPost]
        public async Task<IActionResult> PostPlant([FromBody] PlantAddViewModel plantViewModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var plant = new Plant(plantViewModel.Longitude, plantViewModel.Latitude, plantViewModel.Name, plantViewModel.DeviceId);
        
            //Save image to folder
            //TODO: save to more reliable file provider as: azure blob storage, amazon s3 ed.
            //TODO: use more secure place to store. For HACC its okay
            string filePath = $"plants/{plant.Id}.{plantViewModel.ContentType}";
            System.IO.File.WriteAllBytes(Path.Combine(_hostingEnvironment.ContentRootPath,"wwwroot",filePath), Convert.FromBase64String(plantViewModel.ImageBase64));

            plant.PlantFileUrl = filePath;
            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlant", new { id = plant.Id }, plant);
        }

        private bool PlantExists(Guid id)
        {
            return _context.Plants.Any(e => e.Id == id);
        }
    }
}