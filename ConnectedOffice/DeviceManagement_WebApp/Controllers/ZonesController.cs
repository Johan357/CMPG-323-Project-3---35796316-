﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeviceManagement_WebApp.Data;
using DeviceManagement_WebApp.Models;
using DeviceManagement_WebApp.Repository;

namespace DeviceManagement_WebApp.Controllers
{
    public class ZonesController : Controller
    {
        private readonly IZoneRepository _zoneRepository;                       //assign interface class 

        public ZonesController(IZoneRepository zoneRepository)
        {
            _zoneRepository = zoneRepository;
        }

        // GET: Zones
        public async Task<IActionResult> Index()
        {
            return View(_zoneRepository.GetAll());                      //added use of repo class
        }
        // GET: Zones/Details/5
            public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zoneRepository.GetById((Guid)id);

            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // GET: Zones/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Zones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            zone.ZoneId = Guid.NewGuid();

            _zoneRepository.Add(zone);                      //Repository class add zone
            _zoneRepository.SaveChanges();                  //Repository save changes method

            return RedirectToAction(nameof(Index));
        }

        // GET: Zones/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zoneRepository.GetById((Guid)id);     //Implemented Repository
                        
            if (zone == null)
            {
                return NotFound();
            }
            return View(zone);
        }

        // POST: Zones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ZoneId,ZoneName,ZoneDescription,DateCreated")] Zone zone)
        {
            if (id != zone.ZoneId)
            {
                return NotFound();
            }

            try
            {
                _zoneRepository.Update(zone);
                _zoneRepository.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ZoneExists(zone.ZoneId))
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

        // GET: Zones/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zone = _zoneRepository.GetById((Guid)id);

            if (zone == null)
            {
                return NotFound();
            }

            return View(zone);
        }

        // POST: Zones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var category = _zoneRepository.GetById(id);
            _zoneRepository.Remove(category);
            _zoneRepository.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        private bool ZoneExists(Guid id)
        {
            var results = _zoneRepository.Find(e => e.ZoneId == id);

            if (results == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
