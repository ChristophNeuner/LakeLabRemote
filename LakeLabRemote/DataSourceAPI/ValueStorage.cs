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
            Enums.SensorTypes sensorType = model.SensorType;

            List<ValueItemModel> valueItemsToSave = model.Items;
            IEnumerable<Value> values = await _dbContext.QueryValuesAsync(p => p, sensorType);
            if (values.Count() != 0)
            {
                DateTime latest = values.Select(p => p.Timestamp).Max();
                valueItemsToSave = model.Items.Where(d => d.Timestamp > latest).ToList();
            }

            List<Device> deviceList = (List<Device>)await _dbContext.QueryDevicesAsync(model.DeviceName);
            Device device;
            if (deviceList.Count() == 0)
            {
                device = new Device(model.DeviceName, "not defined", "not defined", Enums.Depth.NotDefined);
                await _deviceStorage.SaveNewDeviceToDbAsync(device);
            }
            else
            {
                //TODO check for every value by its timestamp, which device is the correct one and do not just take the latest.

                device = deviceList.MaxBy(p => p.TimeOfCreation);
            }

            List<Value> valuesToSave = new List<Value>();

            foreach (var value in valueItemsToSave)
            {
                valuesToSave.Add(new Value(value.Timestamp, device, value.Data, sensorType));
            }

            await _dbContext.Values.AddRangeAsync(valuesToSave);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAllValuesAsync()
        {
            _dbContext.Values.RemoveRange(await _dbContext.Values.ToListAsync());
            await _dbContext.SaveChangesAsync();
        }
    }
}
