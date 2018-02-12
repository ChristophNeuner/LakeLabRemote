using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LakeLabLib;
using LakeLabRemote.DataSource;
using LakeLabRemote.Models;
using Microsoft.EntityFrameworkCore;
using MoreLinq;

namespace LakeLabRemote.DataSourceAPI
{
    public class ValueStorage
    {
        private LakeLabDbContext _dbContext;
        private DeviceStorage _deviceStorage;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context">DbContext provided by Dependency Injection.</param>
        public ValueStorage(LakeLabDbContext context, DeviceStorage ds)
        {
            _dbContext = context;
            _deviceStorage = ds;
        }

        public async Task SaveValuesToDbAsync(ValueModel model)
        {
            if (model.SensorType == "do")
            {
                List<ValueItemModel> valueItemsToSave = model.Items;
                IEnumerable<Value> valuesDO = await _dbContext.QueryValuesAsync(p => p);
                if (valuesDO.Count() != 0)
                {
                    DateTime latest = valuesDO.Select(p => p.Timestamp).Max();
                    valueItemsToSave = model.Items.Where(d => d.Timestamp > latest).ToList();
                }

                List<Device> deviceList = (List<Device>)await _dbContext.QueryDevicesAsync(model.DeviceName);
                Device device;
                if (deviceList.Count() == 0)
                {
                    device = new Device(model.DeviceName, "not defined", "not defined", "not defined");
                    await _deviceStorage.SaveNewDeviceToDbAsync(device);
                    await _dbContext.Devices.AddAsync(device);
                    await _dbContext.SaveChangesAsync();

                }
                else
                {
                    //TODO check for every value by its timestamp, which device is the correct one and do not just take the latest.

                    device = deviceList.MaxBy(p => p.TimeOfCreation);
                }

                List<ValueDO> valuesDoToSave = new List<ValueDO>();
                List<ValueTemp> valuesTempToSave = new List<ValueTemp>();

                foreach (var value in valueItemsToSave)
                {
                    valuesDoToSave.Add(new ValueDO(value.Timestamp, device, value.Data, value.Temperature));
                    valuesTempToSave.Add(new ValueTemp(value.Timestamp, device, value.Temperature));
                }

                await _dbContext.ValuesDO.AddRangeAsync(valuesDoToSave);
                await _dbContext.ValuesTemp.AddRangeAsync(valuesTempToSave);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
