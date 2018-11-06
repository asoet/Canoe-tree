using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlantHunter.Mobile.Web.Data.Models;
using web.Data;
using Microsoft.Azure.NotificationHubs;

namespace PlantHunter.Web.Controllers
{
    public class PlantsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlantsController(ApplicationDbContext context)
        {
            _context = context;
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

                    await SendPushNotificationToUserAsync(plantDb.DeviceId, plantDb.Points);
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

        private async Task SendPushNotificationToUserAsync(string deviceId, long points)
        {
            var deviceUser = await _context.DeviceUsers.FirstOrDefaultAsync(f => f.DeviceId == deviceId);
            if (deviceUser == null)
                return;

            // Create a new Notification Hub client.
            NotificationHubClient hub = NotificationHubClient
            .CreateClientFromConnectionString(
                "Endpoint=sb://hacc2018.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=pnK4e5heYeyG4pXz4wdmTdtKjmGnqHBR3W8XOG5rBkg=", "hacc2018");

            string[] userTag = new string[1];
            userTag[0] = "username:" + deviceUser.PushNotificationId;

            // Send the message so that all template registrations that contain "messageParam"
            // receive the notifications. This includes APNS, GCM, WNS, and MPNS template registrations.
            Dictionary<string, string> templateParams = new Dictionary<string, string>
            {
                ["messageParam"] = "You plant submission was updated. You received "+points +" points"
            };

            try
            {
                // Send the push notification and log the results.
                var result = await hub.SendTemplateNotificationAsync(templateParams, userTag);
            }
            catch (System.Exception)
            {
            }
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
