using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LakeLabRemote.DataSource;
using LakeLabRemote.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

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

        [HttpPost]
        public string Index([FromBody]ValueViewModel model)
        {
            return $"device-name: {model.DeviceName}{Environment.NewLine}{model.Items.Select(p=> $"{p.Timestamp}: {p.Data}").Aggregate((e,c) => e + Environment.NewLine + c)}";
        }
    }

    public class ValueItemViewModel
    {
        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("data")]
        public float Data { get; set; }
    }

    public sealed class ValueViewModel
    {
        [JsonProperty("sensorType")]
        public string SensorType { get; set; }

        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }

        [JsonProperty("data")]
        public List<ValueItemViewModel> Items { get; } = new List<ValueItemViewModel>();
    }
}
