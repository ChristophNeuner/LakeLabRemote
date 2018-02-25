using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LakeLabRemote.Models.ViewModels
{
    public class AppUserViewModel
    {
        private AppUser _appUser;
        private IEnumerable<Device> _devices;
        public AppUserViewModel(AppUser user, IEnumerable<Device> devices)
        {
            _appUser = user;
            _devices = devices;
        }

        public AppUser AppUser
        {
            get { return _appUser; }
        }
        public IEnumerable<Device> Devices
        {
            get { return _devices; }
        }
    }
}
