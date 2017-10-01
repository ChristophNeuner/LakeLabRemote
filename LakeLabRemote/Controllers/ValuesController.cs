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
        private ValuesDbContext valuesDbContext;
        private DevicesDbContext devicesDbContext;

        public ValuesController(ValuesDbContext valuesContext, DevicesDbContext devicesContext)
        {
            valuesDbContext = valuesContext;
            devicesDbContext = devicesContext;
        }

        [HttpPost]
        public string Index([FromBody]ValueModel model)
        {
            if (model == null)
                return "The provided data model is null.";

            if (model.SensorType == "do")
            {
                List<ValueItemModel> ValueItemsToSave = model.Items;
                if (valuesDbContext.ValuesDO.Count() != 0)
                {
                    DateTime latest = valuesDbContext.ValuesDO.ToList().Max(r => r.Timestamp);
                    ValueItemsToSave = model.Items.Where(d => d.Timestamp > latest).ToList();
                }

                List<ValueDO> valuesDoToSave = new List<ValueDO>();
                List<Device> deviceList = devicesDbContext.Devices.Where(d => d.Name == model.DeviceName).ToList();
                Device device;
                if (deviceList.Count() == 0)
                {
                    device = new Device(model.DeviceName);
                    devicesDbContext.Add(device);
                    devicesDbContext.SaveChanges();
                }
                else
                {
                    device = deviceList.First();
                }
                
                foreach (var value in ValueItemsToSave)
                {
                    valuesDoToSave.Add(new ValueDO(value.Timestamp, device, value.Data));
                }

                valuesDbContext.ValuesDO.AddRange(valuesDoToSave);
                valuesDbContext.SaveChanges();

                if (valuesDoToSave.Count() != 0)
                {
                    return $"device-name: {model.DeviceName}{Environment.NewLine}sensor-type: {model.SensorType}{Environment.NewLine}{valuesDoToSave.Select(p => $"{p.Timestamp}: {p.Data}").Aggregate((e, c) => e + Environment.NewLine + c)}";
                }
                else
                    return "All values already existed";
                
            }
            else
            {
                return "no success";
            }
            //return $"device-name: {model.DeviceName}{Environment.NewLine}sensor-type: {model.SensorType}{Environment.NewLine}{model.Items.Select(p => $"{p.Timestamp}: {p.Data}").Aggregate((e, c) => e + Environment.NewLine + c)}";
        }
    }

    public class ValueItemModel
    {
        public ValueItemModel(DateTime timestamp, float data)
        {
            Timestamp = timestamp;
            Data = data;
        }

        [JsonProperty("timestamp")]
        public DateTime Timestamp { get; set; }

        [JsonProperty("data")]
        public float Data { get; set; }
    }

    public sealed class ValueModel
    {
        public ValueModel(string deviceName, string sensorType)
        {
            DeviceName = deviceName;
            SensorType = sensorType;
        }

        [JsonProperty("deviceName")]
        public string DeviceName { get; set; }

        [JsonProperty("sensorType")]
        public string SensorType { get; set; }

        [JsonProperty("items")]
        public List<ValueItemModel> Items { get; } = new List<ValueItemModel>();
    }
}
