using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LakeLabRemote.DataSource;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LakeLabRemote.Controllers
{
    [Authorize(Roles = "Admins")]
    public class LoggingController : Controller
    {
        private readonly LoggingDbContext _loggingDbContext;

        public LoggingController(LoggingDbContext loggingDbContext)
        {
            _loggingDbContext = loggingDbContext;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _loggingDbContext.UserRequests.ToListAsync());
        }
    }
}
