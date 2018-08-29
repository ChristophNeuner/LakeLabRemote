using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using LakeLabRemote.DataSource;
using LakeLabRemote.Models;
using LakeLabLib;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using MoreLinq;
using Microsoft.AspNetCore.Authorization;
using LakeLabRemote.DataSourceAPI;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LakeLabRemote.Controllers
{
    //[Authorize(Roles = "Admins")]
    public class apiController : Controller
    {
        private LakeLabDbContext _dbContext;
        private ValueStorage _valueStorage;
        private DeviceStorage _deviceStorage;

        public apiController(LakeLabDbContext context, ValueStorage vs, DeviceStorage ds)
        {
            _dbContext = context;
            _valueStorage = vs;
            _deviceStorage = ds;
        }


        /// <summary>
        /// The raspberry pis send their data to this method.
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<string> ReceiveValues([FromBody]List<ValueModel> models)
        {
            if (models == null)
                throw new ArgumentNullException(nameof(models));

            string result = "stored: ";

            foreach (ValueModel elem in models)
            {
                await _deviceStorage.SaveDeviceIpAsync(elem.DeviceName, HttpContext.Connection.RemoteIpAddress.ToString());
                await _valueStorage.SaveValuesToDbAsync(elem);
                result += elem.SensorType.ToString() + " ";
            }

            return "success";
            //return result;
            //return $"device-name: {model.DeviceName}{Environment.NewLine}sensor-type: {model.SensorType}{Environment.NewLine}{model.Items.Select(p => $"{p.Timestamp}: {p.Data}").Aggregate((e, c) => e + Environment.NewLine + c)}";
        }

        /// <summary>
        /// The WebApp's JavaScript calls this method to get values in a Json format for plotting.
        /// </summary>
        /// <param name="deviceId">The Id of the device entity you want to get the values from</param>
        /// <param name="sensorType"></param>
        /// <param name="days">From how many days ago unitl today you want to have the values.</param>
        /// <returns>ValueModel as Json file</returns>
        [HttpGet]
        public async Task<JsonResult> GetValuesFromLastNDaysAsJsonAsync(string deviceId, int sensorType = (int)Enums.SensorTypes.Dissolved_Oxygen, int days = 7)
        {
            IEnumerable<Value> values = await _dbContext.Values.Where(p => p.Device.Id.ToString() == deviceId && (int)p.SensorType == sensorType && p.Timestamp >= DateTime.Today.AddDays(-1 * days)).ToListAsync();            
            List<PlotItemModel> plotItemModels = new List<PlotItemModel>();
            foreach (var elem in values)
            {
                plotItemModels.Add(new PlotItemModel(GetJavascriptTimestamp(elem.Timestamp), elem.Data));
            }
            Device device = await _dbContext.QueryDevicesAsync(new Guid(deviceId));
            PlotModel plotModel = new PlotModel(device.Name, (Enums.SensorTypes)sensorType);
            plotModel.Items.AddRange(plotItemModels);
            return Json(plotModel);
        }

        public async Task<JsonResult> GetValuesBetweenTwoDatesAsJsonAsync(string deviceId, Enums.SensorTypes sensorType, DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public static long GetJavascriptTimestamp(DateTime input)
        {
            TimeSpan span = new TimeSpan(DateTime.Parse("1/1/1970").Ticks);
            DateTime time = input.Subtract(span);
            return time.Ticks/10000;
        }
    }
}