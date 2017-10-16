using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.Models.ViewModels
{
    public class AppUserDevicesEditViewModel
    {
        public AppUserDevicesEditViewModel(AppUser user, List<Device> members, List<Device> nonMembers)
        {
            User = user;
            Members = members;
            NonMembers = nonMembers;
        }
        public AppUser User { get; set; }
        public List<Device> Members { get; set; }
        public List<Device> NonMembers { get; set; }
    }
}
