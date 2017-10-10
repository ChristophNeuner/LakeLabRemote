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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace LakeLabRemote.Controllers
{
    //[Authorize(Roles = "Admins")]
    public class ValuesController : Controller
    {
        private LakeLabDbContext _dbContext;

        public ValuesController(LakeLabDbContext valuesContext, DataSource.LakeLabDbContext devicesContext)
        {
            _dbContext = valuesContext;
        }

        [HttpPost]
        public async Task<string> Index([FromBody]ValueModel model)
        {
            if (model == null)
                return "The provided data model is null.";

            if (model.SensorType == "do")
            {
                List<ValueItemModel> ValueItemsToSave = model.Items;
                if ((await _dbContext.ValuesDO.CountAsync()) != 0)
                {
                    DateTime latest = await _dbContext.QueryValuesAsync(queryable => queryable.Select(p => p.Timestamp).MaxAsync());
                    ValueItemsToSave = model.Items.Where(d => d.Timestamp > latest).ToList();
                }

                List<ValueDO> valuesDoToSave = new List<ValueDO>();
                List<Device> deviceList = _dbContext.Devices.Where(d => d.Name == model.DeviceName).ToList();
                Device device;
                if (deviceList.Count() == 0)
                {
                    return "The device does not exist!";
                }
                else
                {
                    //TODO check for every value by its timestamp, which device is the correct one and do not just take the latest.

                    device = deviceList.MaxBy(p => p.TimeOfCreation);
                }

                foreach (var value in ValueItemsToSave)
                {
                    valuesDoToSave.Add(new ValueDO(value.Timestamp, device, value.Data));
                }

                _dbContext.ValuesDO.AddRange(valuesDoToSave);
                _dbContext.SaveChanges();

                if (valuesDoToSave.Count() != 0)
                {
                    return $"device-name: {model.DeviceName}{Environment.NewLine}sensor-type: {model.SensorType}{Environment.NewLine}{valuesDoToSave.Select(p => $"{p.Timestamp}: {p.Data}").Aggregate((e, c) => e + Environment.NewLine + c)}";
                }
                else
                {
                    return "All values already existed";
                }
            }
            else
            {
                return "no success";
            }
            //return $"device-name: {model.DeviceName}{Environment.NewLine}sensor-type: {model.SensorType}{Environment.NewLine}{model.Items.Select(p => $"{p.Timestamp}: {p.Data}").Aggregate((e, c) => e + Environment.NewLine + c)}";
        }
    }
}