using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LakeLabRemote.DataSource;
using LakeLabRemote.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LakeLabRemote.Controllers
{
    [Authorize(Roles = "Admins")]
    public class SensorsController : Controller
    {
        private LakeLabDbContext _dbContext;

        public SensorsController(LakeLabDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.Sensors.ToListAsync());
        }
    }
}
