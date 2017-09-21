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
        ValuesDbContext valuesDbContext;

        public HomeController(ValuesDbContext context)
        {
            valuesDbContext = context;
        }

        public IActionResult Index()
        {
            return View(valuesDbContext.Test.ToList());
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
