using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.Models
{
    public class AppUserDevice
    {
        private AppUserDevice() { }
        public AppUserDevice(Guid appUserId, Guid deviceId)
        {
            AppUserId = appUserId;
            DeviceId = deviceId;
        }
        public Guid AppUserId { get; set; }
        public Guid DeviceId { get; set; }
        public AppUser User { get; set; }
        public Device Device { get; set; }
    }
}
