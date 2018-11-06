using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.NotificationHubs;
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

        public class DeviceRegistration
        {
            public string Platform { get; set; }
            public string Handle { get; set; }
            public string[] Tags { get; set; }
        }

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

        //// PUT: api/PlantsApi/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutPlant([FromRoute] Guid id, [FromBody] PlantAddViewModel plantViewModel)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    var plant = _context.Plants.Find(id);
        //    //Update Properties
        //    plant.Longitude = plantViewModel.Longitude;
        //    plant.Latitude = plantViewModel.Latitude;


        //    _context.Entry(plant).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PlantExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/PlantsApi
        [HttpPost]
        public async Task<IActionResult> PostPlant([FromBody] PlantAddViewModel plantViewModel, string pns = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var plant = new Plant(plantViewModel.Longitude, plantViewModel.Latitude, plantViewModel.Name, plantViewModel.DeviceId, plantViewModel.ScientificName, plantViewModel.Family, plantViewModel.EndangeredLevel, plantViewModel.Surrounding, plantViewModel.Description);
        
            //Save image to folder
            //TODO: save to more reliable file provider as: azure blob storage, amazon s3 ed.
            //TODO: use more secure place to store. For HACC its okay
            string filePath = $"plants/{plant.Id}.{plantViewModel.ContentType}";
            System.IO.File.WriteAllBytes(Path.Combine(_hostingEnvironment.ContentRootPath,"wwwroot",filePath), Convert.FromBase64String(plantViewModel.ImageBase64));

            plant.PlantFileUrl = filePath;
            _context.Plants.Add(plant);
            await _context.SaveChangesAsync();

            //Register Device if not registered allready
            await RegisterDeviceAsync(plant.DeviceId, pns);

            return CreatedAtAction("GetPlant", new { id = plant.Id }, plant);
        }

        private async Task<string> RegisterDeviceAsync(string deviceId, string pns = null)
        {
            NotificationHubClient hub = NotificationHubClient
           .CreateClientFromConnectionString(
               "Endpoint=sb://hacc2018.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=pnK4e5heYeyG4pXz4wdmTdtKjmGnqHBR3W8XOG5rBkg=", "hacc2018");

            var deviceUser = await _context.DeviceUsers.FirstOrDefaultAsync(f => f.DeviceId == deviceId);
            if (!string.IsNullOrEmpty(deviceUser.PushNotificationId))
                return deviceUser.PushNotificationId;

            string newRegistrationId = null;

            // make sure there are no existing registrations for this push handle (used for iOS and Android)
            if (pns != null)
            {
                var registrations = await hub.GetRegistrationsByChannelAsync(pns, 100);

                foreach (RegistrationDescription registration in registrations)
                {
                    if (newRegistrationId == null)
                    {
                        newRegistrationId = registration.RegistrationId;
                    }
                    else
                    {
                        await hub.DeleteRegistrationAsync(registration);
                    }
                }
            }

            if (newRegistrationId == null)
                newRegistrationId = await hub.CreateRegistrationIdAsync();

            deviceUser.PushNotificationId = newRegistrationId;
            deviceUser.Pns = pns;
            _context.DeviceUsers.Update(deviceUser);
            await _context.SaveChangesAsync();

            return newRegistrationId;
        }

        private bool PlantExists(Guid id)
        {
            return _context.Plants.Any(e => e.Id == id);
        }
    }
}