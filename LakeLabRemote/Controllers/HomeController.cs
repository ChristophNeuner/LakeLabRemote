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
            List<ValueDO> valuesDO = valuesDbContext.ValuesDO.ToList();
            return View(valuesDO);
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
