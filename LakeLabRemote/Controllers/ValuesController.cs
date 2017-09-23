using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LakeLabRemote.DataSource;
using LakeLabRemote.Models;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LakeLabRemote.Controllers
{
    //[Authorize(Roles = "Admins")]
    public class ValuesController : Controller
    {
        ValuesDbContext valuesDbContext;

        public ValuesController(ValuesDbContext context)
        {
            valuesDbContext = context;
        }
        public string Index(JsonObject value)
        {
            if (value != null)
            {
                valuesDbContext.Test.Add(new Test {Id = Guid.NewGuid().ToString(), Value = value.ToString()});
                valuesDbContext.SaveChanges();
                return "success";
            }
            else
            {
                return "no success";
            }
        }
    }
}
