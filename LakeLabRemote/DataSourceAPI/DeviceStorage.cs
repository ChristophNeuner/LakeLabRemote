using LakeLabRemote.DataSource;
using LakeLabRemote.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.DataSourceAPI
{
    public class DeviceStorage
    {
        private LakeLabDbContext _dbContext;

        public DeviceStorage(LakeLabDbContext context)
        {
            _dbContext = context;
        }

        public async Task SaveNewDeviceToDbAsync(Device device)
        {
            await _dbContext.Devices.AddAsync(device);
            await _dbContext.SaveChangesAsync();
        }

        public async Task SaveDeviceIpAsync(string deviceName, string deviceIp)
        {
            if (String.IsNullOrWhiteSpace(deviceName))
                throw new NullReferenceException(nameof(deviceName));
            if (String.IsNullOrWhiteSpace(deviceIp))
                throw new NullReferenceException(nameof(deviceIp));

            List<Device> devices = await _dbContext.Devices.Where(p => p.Name == deviceName).ToListAsync();
            foreach (var device in devices)
            {
                device.Ip = deviceIp;
            }
            _dbContext.Devices.UpdateRange(devices);
            await _dbContext.SaveChangesAsync();
        }
    }
}
