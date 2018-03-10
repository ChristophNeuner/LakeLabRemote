using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LakeLabRemote.Models;
using MoreLinq;

namespace LakeLabRemote.Models.ViewModels
{
    /// <summary>
    /// View Model for the Home/Index page
    /// contains all devices with all entities a user has access to
    /// </summary>
    public class HomeIndexViewModel
    {
        public HomeIndexViewModel(List<HomeIndexViewModelItem> items)
        {
            Items = items;
        }

        /// <summary>
        /// Creates a HomeIndexViewModel from a Dictionary<string, List<Device>> devicesDicitionary.
        /// </summary>
        /// <param name="devicesDicitionary">Key: deviceName, Value: List of devices with that name</param>
        public HomeIndexViewModel(Dictionary<string, List<Device>> devicesDicitionary)
        {
            List<HomeIndexViewModelItem> items = new List<HomeIndexViewModelItem>();
            foreach(var elem in devicesDicitionary)
            {
                Device latestDevice = elem.Value.MaxBy(p => p.TimeOfCreation);
                List<Device> oldDeviceEntities = elem.Value;
                oldDeviceEntities.Remove(latestDevice);

                items.Add(new HomeIndexViewModelItem(latestDevice, oldDeviceEntities));
            }

            Items = items;
        }

        public List<HomeIndexViewModelItem> Items { get; }
    }

    public class HomeIndexViewModelItem
    {
        public HomeIndexViewModelItem(Device latestDeviceEntity, List<Device> oldDeviceEntities)
        {
            LatestDeviceEntity = latestDeviceEntity;
            OldDeviceEntities = oldDeviceEntities;
        }

        public Device LatestDeviceEntity { get; }
        public List<Device> OldDeviceEntities { get; }
    }
}
