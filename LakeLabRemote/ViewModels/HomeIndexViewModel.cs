using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LakeLabRemote.Models;

namespace LakeLabRemote.ViewModels
{
    /// <summary>
    /// View Model for the Home/Index page
    /// contains all devices with all entities a user has access to
    /// </summary>
    public class HomeIndexViewModel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="current">List with dictionaries: key == DeviceName, value: latest entity of that device</param>
        /// <param name="old">key: DeviceName, value: all old entities of that device</param>
        public HomeIndexViewModel(List<Device> current/*, List<Dictionary<string, List<Device>>> old*/)
        {
            CurrentDeviceEntities = current;
            //OldDeviceEntities = old;
        }

        public List<Device> CurrentDeviceEntities { get; set; }
        //public List<Dictionary<string, List<Device>>> OldDeviceEntities { get; set; }
    }
}
