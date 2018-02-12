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
    public class ValuesController : Controller
    {
        private LakeLabDbContext _dbContext;
        private ValueStorage _valueStorage;
        private DeviceStorage _deviceStorage;

        public ValuesController(LakeLabDbContext context, ValueStorage vs, DeviceStorage ds)
        {
            _dbContext = context;
            _valueStorage = vs;
            _deviceStorage = ds;
        }

        [HttpPost]
        public async Task<string> ReceiveValues([FromBody]ValueModel model)
        {
            if (model == null)
                throw new ArgumentNullException(nameof(model));
            if (model.Items == null)
                throw new ArgumentNullException(nameof(model.Items));
            if (model.DeviceName == null)
                throw new ArgumentNullException(nameof(model.DeviceName));
            if (model.SensorType == null)
                throw new ArgumentNullException(nameof(model.SensorType));

            await _deviceStorage.SaveDeviceIpAsync(model.DeviceName, HttpContext.Connection.RemoteIpAddress.ToString());
            await _valueStorage.SaveValuesToDbAsync(model);
            return "Success";
            //return $"device-name: {model.DeviceName}{Environment.NewLine}sensor-type: {model.SensorType}{Environment.NewLine}{model.Items.Select(p => $"{p.Timestamp}: {p.Data}").Aggregate((e, c) => e + Environment.NewLine + c)}";
        }
    }
}