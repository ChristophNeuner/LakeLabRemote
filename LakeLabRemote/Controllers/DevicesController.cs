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
        private DevicesDbContext devicesDbContext;
        public DevicesController(DevicesDbContext context)
        {
            devicesDbContext = context;
        }

        public IActionResult Index()
        {
            return View(devicesDbContext.Devices.ToList());
        }

        public IActionResult CreateDevice() => View();

        [HttpPost]
        public IActionResult CreateDevice(CreateDeviceViewModel model)
        {
            if (devicesDbContext.Devices.ToList().Any(elem => elem.Name == model.Name))
            {
                ModelState.AddModelError("", "A device with the same name already exists");
            }

            if (ModelState.IsValid)
            {              
                Device device = new Device(model.Name, model.Location, model.Depth);
                devicesDbContext.Add(device);
                devicesDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);         
        }

        [HttpPost]
        public IActionResult Delete(string id)
        {   
            Device device = devicesDbContext.Devices.ToList().Find(elem => elem.Name == id);
            devicesDbContext.Remove(device);
            devicesDbContext.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

    }
}