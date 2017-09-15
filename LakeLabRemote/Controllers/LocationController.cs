using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LakeLabRemote.Models.ViewModels;
using LakeLabRemote.DataSource;
using LakeLabRemote.Models;

namespace LakeLabRemote.Controllers
{
    [Authorize(Roles = "Admins")]
    public class LocationController : Controller
    {
        private DevicesDbContext dLDbContext;
        public LocationController(DevicesDbContext dLDb)
        {
            dLDbContext = dLDb;
        }

        [HttpGet]
        public IActionResult Index() => View(dLDbContext.Devices.ToList<Device>());

        //[HttpPost]
        //public IActionResult Location()
        //{
        //    throw new NotImplementedException();
        //}
    }
}
