﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LakeLabRemote.Models.ViewModels;
using LakeLabRemote.DataSource;
using LakeLabRemote.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LakeLabRemote.Controllers
{
    [Authorize(Roles = "Admins")]
    public class DevicesController : Controller
    {
        private AppIdentityDbContext _identityDbContext;
        private LakeLabDbContext _dbContext;
        public DevicesController(LakeLabDbContext context, AppIdentityDbContext identityDbContext)
        {
            _dbContext = context;
            _identityDbContext = identityDbContext;
        }

        public IActionResult Index()
        {
            return View(_dbContext.Devices.ToList());
        }

        public IActionResult CreateDevice() => View();

        [HttpPost]
        public IActionResult CreateDevice(Device model)
        {
            if (_dbContext.Devices.ToList().Any(elem => elem.Name == model.Name))
            {
                ModelState.AddModelError("", "A device with the same name already exists");
            }

            if (ModelState.IsValid)
            {
                Device device = new Device(model.Name, model.Lake, model.Location, model.Depth);
                _dbContext.Add(device);
                _dbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);         
        }

        [HttpPost]
        public IActionResult Delete(Guid id)
        {
            List<Device> devices = _dbContext.Devices.Where(p => p.Guid == id).ToList();
            _dbContext.Devices.RemoveRange(devices);
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(Guid id)
        {
            Device device = new Device();
            if(id == null)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                device = _dbContext.Devices.First(d => d.Guid == id);
                if(device == null)
                {
                    ModelState.AddModelError("", "The device no longer exists.");
                }
            }
            if(ModelState.IsValid)
            {
                return View(device);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        public IActionResult Edit(Device device)
        {
            Device newDevice = new Device(device.Name, device.Lake, device.Location, device.Depth);
            _dbContext.Devices.AddAsync(newDevice);
            foreach(AppUser user in _identityDbContext.Users)
            {
                if(user.HasDevice(device))
                {
                    user.Devices.Add(newDevice);
                }
            }
            _dbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}
