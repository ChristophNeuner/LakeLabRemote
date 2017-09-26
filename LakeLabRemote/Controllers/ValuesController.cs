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
        public string Index([FromBody] string deviceName, [FromBody] string sensorType, [FromBody] List<ValueViewModel> data)
        {
            return deviceName + " " + sensorType + " " + data.First().ToString();
        }
    }

    public class ValueViewModel
    {
        DateTime timestamp;
        float data;

        public ValueViewModel(DateTime ts, float d)
        {
            timestamp = ts;
            data = d;
        }
    }
}
