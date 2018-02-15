using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LakeLabRemote.DataSource;
using LakeLabRemote.Models;
using Microsoft.EntityFrameworkCore;
using LakeLabLib;
using LakeLabRemote.DataSourceAPI;

namespace LakeLabRemote.Controllers
{
    [Authorize(Roles = "Admins")]
    public class HomeController : Controller
    {
        LakeLabDbContext _dbContext;
        ValueStorage _valueStorage;

        public HomeController(LakeLabDbContext context, ValueStorage vs)
        {
            _dbContext = context;
            _valueStorage = vs;
        }

        public async Task<IActionResult> Index()
        {
            var valuesDO = await _dbContext.QueryValuesAsync(p => p, Enums.SensorTypes.Dissolved_Oxygen);
            return View(valuesDO.Reverse<Value>());
        }

        public IActionResult Error()
        {
            return View();
        }

        public async Task<IActionResult> DeleteAllValues()
        {
            await _valueStorage.DeleteAllValuesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}