using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.Models
{
    public class AppUserDevice
    {
        private AppUserDevice() { }
        public AppUserDevice(string appUserId, Guid deviceId)
        {
            AppUserId = appUserId;
            DeviceId = deviceId;
            ComposedKey = appUserId + deviceId;
        }
        public string AppUserId { get; set; }
        public Guid DeviceId { get; set; }

        [Key]
        public string ComposedKey { get; private set; }
    }
}
