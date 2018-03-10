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
        /// Gets all device entities a user has access to as an unsorted list.
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Device>> GetAllDeviceEntitiesForUserAsUnsortedListAsync(AppUser user)
        {
            List<AppUserDevice> associations = await _dbContext.AppUserDeviceAssociation.Where(p => p.AppUserId == user.Id).ToListAsync();
            List<Device> allDevices = await _dbContext.Devices.ToListAsync();
            List<Device> accessibleDevices = new List<Device>();
            foreach (var assoc in associations)
            {
                foreach (var device in allDevices)
                {
                    if (assoc.DeviceId == device.Id)
                        accessibleDevices.Add(device);
                }
            }
            return accessibleDevices;
        }

        /// <summary>
        /// checks if a user has access to a specific device entity
        /// </summary>
        /// <param name="context"></param>
        /// <param name="user"></param>
        /// <param name="device"></param>
        /// <returns></returns>
        public async Task<bool> IsDeviceAccessibleForUserAsync(AppUser user, Device device)
        {
            if (user == null)
                throw new NullReferenceException(nameof(user));

            if (device == null)
                throw new NullReferenceException(nameof(device));

            IEnumerable<Device> accessibleDevices = await GetAllDeviceEntitiesForUserAsUnsortedListAsync(user);
            if (accessibleDevices.Where(d => d.Id == device.Id).Count() == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Gets all entities of all devices a user has access to in form of a dictionary where the key is the deviceName and the value a list of all device entities with that name !UNSORTED!
        /// </summary>
        /// <param name="context">The database Context class</param>
        /// <param name="userId">The user's id you want to get the devices for.</param>
        /// <returns>Returns a list of dictionaries. Key: DeviceName, Value: list of all device entities with that name unsorted</Device></returns>
        public async Task<Dictionary<string, List<Device>>> GetAllDeviceEntitiesForUserAsDictionaryAsync(AppUser user)
        {
            List<Device> accessibleDevices = (List<Device>)await GetAllDeviceEntitiesForUserAsUnsortedListAsync(user);
            List<string> deviceNamesNotDistinct = new List<string>();
            foreach (var device in accessibleDevices)
            {
                deviceNamesNotDistinct.Add(device.Name);
            }
            List<string> deviceNamesDistinct = deviceNamesNotDistinct.Distinct().ToList();
            Dictionary<string, List<Device>> result = new Dictionary<string, List<Device>>();

            foreach (var name in deviceNamesDistinct)
            {
                result.Add(name, accessibleDevices.Where(device => device.Name == name).ToList());
            }

            return result;
        }
    }
}
