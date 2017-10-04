using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LakeLabRemote.DataSource;
using LakeLabRemote.Models;

namespace LakeLabRemote.Controllers
{
    [Authorize(Roles = "Admins")]
    public class HomeController : Controller
    {
        LakeLabDbContext _dbContext;

        public HomeController(LakeLabDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            var valuesDO = await _dbContext.QueryValuesAsync(p => p);
            return View(valuesDO);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
