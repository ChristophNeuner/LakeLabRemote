using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.Models.ViewModels
{
    public class AppUserDevicesEditViewModel
    {
        public AppUserDevicesEditViewModel(AppUser user, List<Device> accessibleDevices, List<Device> notAccessibleDevices)
        {
            User = user;
            AccessibleDevices = accessibleDevices;
            NotAccessibleDevices = notAccessibleDevices;
        }
        public AppUser User { get; set; }
        public List<Device> AccessibleDevices { get; set; }
        public List<Device> NotAccessibleDevices { get; set; }
    }
}
