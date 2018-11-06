using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Azure.NotificationHubs;
using Microsoft.EntityFrameworkCore;
using PlantHunter.Mobile.Web.Data.Models;
using PlantHunter.Web.NotificationHubs;
using web.Data;

namespace PlantHunter.Web.Controllers
{
    public class PlantsController : Controller
    {
        private NotificationHubProxy _notificationHubProxy;
        private readonly ApplicationDbContext _context;

        public PlantsController(ApplicationDbContext context)
        {
            _context = context;
            _notificationHubProxy = new NotificationHubProxy(_context);
        }

        // GET: Plants
        public async Task<IActionResult> Index()
        {
            return View(await _context.Plants.ToListAsync());
        }

        // GET: Plants/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        //// GET: Plants/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Plants/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Longitude,Latitude,Id,PlantFileUrl,Name,DeviceId,Points")] Plant plant)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        plant.Id = Guid.NewGuid();
        //        _context.Add(plant);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(plant);
        //}

        // GET: Plants/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plants.FindAsync(id);
            if (plant == null)
            {
                return NotFound();
            }
            return View(plant);
        }

        // POST: Plants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [FromForm]Plant plant)
        {
            if (id != plant.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var plantDb = await _context.Plants
                        .FirstOrDefaultAsync(m => m.Id == id);
                    plantDb.Name = plant.Name;
                    plantDb.PlantFileUrl = plant.PlantFileUrl;
                    plantDb.Points = plant.Points;
                    plantDb.DeviceId = plant.DeviceId;
                    plantDb.ScientificName = plant.ScientificName;
                    plantDb.Family = plant.Family;
                    plantDb.Description = plant.Description;
                    plantDb.Surrounding = plant.Surrounding;
                    plantDb.EndangeredLevel = plant.EndangeredLevel;

                    _context.Update(plantDb);
                    await _context.SaveChangesAsync();

                    var deviceId = await _context.PushRegistrations.FirstOrDefaultAsync(f => f.DeviceId == plantDb.DeviceId);
                    if(deviceId != null)
                    {
                        HubResponse<NotificationOutcome> pushDeliveryResult = await _notificationHubProxy.SendNotification(new NotificationHubs.Notification
                        {
                            Content = $"One of your plants is updated: {plantDb.Points} points",
                            Tags = new string[1] { deviceId.Tag },
                            Platform = deviceId.MobilePlatform
                        });
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlantExists(plant.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(plant);
        }

        // GET: Plants/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var plant = await _context.Plants
                .FirstOrDefaultAsync(m => m.Id == id);
            if (plant == null)
            {
                return NotFound();
            }

            return View(plant);
        }

        // POST: Plants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var plant = await _context.Plants.FindAsync(id);
            _context.Plants.Remove(plant);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlantExists(Guid id)
        {
            return _context.Plants.Any(e => e.Id == id);
        }
    }
}
