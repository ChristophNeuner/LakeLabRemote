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
        /// <summary>
        /// just an object to use a  "lock" statement
        /// </summary>
        //private Object lockObject = new Object();

        /// <summary>
        /// A list of all the received values that are "waiting" to be saved in the database.
        /// </summary>
        private List<ValueModel> _valueModels;

        /// <summary>
        /// Only if this is false, a thread is allowed to take ValueModel from _valueModels and save it to the database. 
        /// This is to guarantee, that only one thread at a time writes new values to the db.
        /// </summary>
        //private bool _locked = false;

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
            _valueModels = new List<ValueModel>();
        }



        /// <summary>
        /// Receives new values from the controller and saves them in _valueModels.
        /// </summary>
        /// <param name="valueModel"></param>
        public async void QueueNewValues(ValueModel valueModel)
        {
            _valueModels.Add(valueModel);
            await SaveValuesToDbAsync();
        }


        private async Task SaveValuesToDbAsync()
        {
            while (_valueModels.Count() > 0)
            {
                ValueModel model = _valueModels.First();

                if (model.SensorType == "do")
                {
                    List<ValueItemModel> ValueItemsToSave = model.Items;
                    if ((_dbContext.ValuesDO.Count()) != 0)
                    {
                        DateTime latest = await _dbContext.QueryValuesAsync(queryable => queryable.Select(p => p.Timestamp).MaxAsync());
                        ValueItemsToSave = model.Items.Where(d => d.Timestamp > latest).ToList();
                    }

                    List<Device> deviceList = (List<Device>)await _dbContext.QueryDevicesAsync(model.DeviceName);
                    Device device;
                    if (deviceList.Count() == 0)
                    {
                        device = new Device(model.DeviceName, "not defined", "not defined", "not defined");
                        await _deviceStorage.SaveNewDeviceToDbAsync(device);

                    }
                    else
                    {
                        //TODO check for every value by its timestamp, which device is the correct one and do not just take the latest.

                        device = deviceList.MaxBy(p => p.TimeOfCreation);
                    }

                    List<ValueDO> valuesDoToSave = new List<ValueDO>();
                    List<ValueTemp> valuesTempToSave = new List<ValueTemp>();

                    foreach (var value in ValueItemsToSave)
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


        /// <summary>
        /// new thread
        /// </summary>
        //private void SaveValuesToDb()
        //{
        //    lock (lockObject)
        //    {
        //        if (_locked == true)
        //            return;
        //        else
        //            _locked = true;
        //    }

        //    Task.Run(function: async () => {
        //        while(_valueModels.Count() > 0)
        //        {
        //            ValueModel model = _valueModels.First();

        //            if (model.SensorType == "do")
        //            {
        //                List<ValueItemModel> ValueItemsToSave = model.Items;
        //                if ((_dbContext.ValuesDO.Count()) != 0)
        //                {
        //                    DateTime latest = _dbContext.QueryValues(queryable => queryable.Select(p => p.Timestamp)).Max();
        //                    ValueItemsToSave = model.Items.Where(d => d.Timestamp > latest).ToList();
        //                }

        //                List<Device> deviceList = _dbContext.Devices.Where(d => d.Name == model.DeviceName).ToList();
        //                Device device;
        //                if (deviceList.Count() == 0)
        //                {
        //                    device = new Device(model.DeviceName, "not defined", "not defined", "not defined");
        //                    await _deviceStorage.SaveNewDeviceToDbAsync(device);

        //                }
        //                else
        //                {
        //                    //TODO check for every value by its timestamp, which device is the correct one and do not just take the latest.

        //                    device = deviceList.MaxBy(p => p.TimeOfCreation);
        //                }

        //                List<ValueDO> valuesDoToSave = new List<ValueDO>();
        //                List<ValueTemp> valuesTempToSave = new List<ValueTemp>();

        //                foreach (var value in ValueItemsToSave)
        //                {
        //                    valuesDoToSave.Add(new ValueDO(value.Timestamp, device, value.Data, value.Temperature));
        //                    valuesTempToSave.Add(new ValueTemp(value.Timestamp, device, value.Temperature));
        //                }

        //                _dbContext.ValuesDO.AddRange(valuesDoToSave);
        //                _dbContext.ValuesTemp.AddRange(valuesTempToSave);
        //                _dbContext.SaveChanges();
        //            }
        //        }

        //        _locked = false;

        //    });
        //}
    }
}
