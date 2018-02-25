using LakeLabRemote.DataSource;
using LakeLabRemote.Models;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Gets a list of the devices the currently logged in user has access to. 
        /// It only gets the latest entities.
        /// </summary>
        /// <param name="currentUser"></param>
        /// <returns></returns>
        public async Task<List<Device>> GetCurrentUsersDevicesAsync(AppUser currentUser)
        {
            throw new NotImplementedException();
        }
    }
}
