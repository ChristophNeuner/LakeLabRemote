﻿using LakeLabLib;
using LakeLabRemote.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.DataSource
{
    public static class LakeLabContextExtension
    {
        public static async Task<IEnumerable<Device>> QueryDevicesAsync(this LakeLabDbContext context, string name)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            return await context.Devices.Where(p => p.Name == name).ToListAsync();
        }

        public static async Task<Device> QueryDevicesAsync(this LakeLabDbContext context, Guid deviceId)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));
            if (deviceId == null)
                throw new ArgumentNullException(nameof(deviceId));

            List<Device> devices = await context.Devices.Where(p => p.Id == deviceId).ToListAsync();
            return devices.First();
        }

        public static async Task<IEnumerable<T>> QueryValuesAsync<T>(this LakeLabDbContext context, Func<IQueryable<Value>, IQueryable<T>> queryShaper, Enums.SensorTypes sensorType)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (queryShaper == null)
            {
                throw new ArgumentNullException(nameof(queryShaper));
            }

            return await queryShaper(context.Values.Include(p => p.Device).Where(p => p.SensorType == sensorType)).ToListAsync();        
        }

        public static Task<T> QueryValuesAsync<T>(this LakeLabDbContext context, Func<IQueryable<Value>, Task<T>> queryShaper, Enums.SensorTypes sensorType)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (queryShaper == null)
            {
                throw new ArgumentNullException(nameof(queryShaper));
            }

            return queryShaper(context.Values.Include(p => p.Device).Where(p => p.SensorType == sensorType));
        }

        public static async Task<IEnumerable<Device>> QueryAppUserDeviceAssociationAsync(this LakeLabDbContext context, string userId)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            List<AppUserDevice> associations = await context.AppUserDeviceAssociation.Where(p => p.AppUserId == userId).ToListAsync();
            List<Device> allDevices = await context.Devices.ToListAsync();
            List<Device> accessibleDevices = new List<Device>();
            foreach (var elem in associations)
            {
                accessibleDevices.Add(allDevices.FirstOrDefault(p => p.Id == elem.DeviceId));
            }
            return accessibleDevices;
        }

        public static async Task<bool> IsDeviceAccessibleForUserAsync(this LakeLabDbContext context, AppUser user, Device device)
        {
            if (user == null)
                throw new NullReferenceException(nameof(user));

            if (device == null)
                throw new NullReferenceException(nameof(device));

            IEnumerable<Device> accessibleDevices = await QueryAppUserDeviceAssociationAsync(context, user.Id);
            if (accessibleDevices.Where(d => d.Id == device.Id).Count() == 0)
                return false;
            else
                return true;
        }       
    }
}
