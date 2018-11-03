using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LakeLabRemote.DataSource;
using LakeLabRemote.Models;
using Microsoft.EntityFrameworkCore;
using LakeLabRemote.DataSourceAPI;

namespace LakeLabRemote.Controllers
{
    [Authorize(Roles = "Admins")]
    public class DevicesController : Controller
    {
        private AppIdentityDbContext _identityDbContext;
        private LakeLabDbContext _dbContext;
        private DeviceStorage _deviceStorage;
        public DevicesController(LakeLabDbContext context, AppIdentityDbContext identityDbContext, DeviceStorage deviceStorage)
        {
            _dbContext = context;
            _identityDbContext = identityDbContext;
            _deviceStorage = deviceStorage;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Devices.ToListAsync());
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
                _dbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(model);         
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
                device = _dbContext.Devices.First(d => d.Id == id);
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
        public async Task<IActionResult> Edit(Device device)
        {
            Device newDevice = new Device(device.Name, device.Lake, device.Location, device.Depth);
            await _dbContext.Devices.AddAsync(newDevice);
            List<AppUserDevice> newAppUserAssociations = new List<AppUserDevice>();
            foreach(AppUser user in _identityDbContext.Users)
            {
                if(await _deviceStorage.IsDeviceAccessibleForUserAsync(user, device))
                {
                    newAppUserAssociations.Add(new AppUserDevice(user.Id, newDevice.Id));
                }
            }
            await _dbContext.AppUserDeviceAssociation.AddRangeAsync(newAppUserAssociations);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        //[HttpPost]
        //public async Task<IActionResult> Delete(Guid id)
        //{
        //    List<Value> valuesToDelete = await _dbContext.Values.Where(v => v.Device.Id == id).ToListAsync();
        //    _dbContext.Values.RemoveRange(valuesToDelete);
        //    List<Device> devicesToDelete = await _dbContext.Devices.Where(p => p.Id == id).ToListAsync();
        //    _dbContext.Devices.RemoveRange(devicesToDelete);
        //    List<AppUserDevice> associationsToDelete = await _dbContext.AppUserDeviceAssociation.Where(p => p.DeviceId == id).ToListAsync();
        //    _dbContext.AppUserDeviceAssociation.RemoveRange(associationsToDelete);
        //    await _dbContext.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

    }
}
