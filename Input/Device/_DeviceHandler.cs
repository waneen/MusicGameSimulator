using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGS.Input
{
    public class DeviceHandler
    {
        private Dictionary<string, Device> Devices { get; set; }

        public DeviceHandler()
        {
            //デバイスを検索、Devicesに格納していく
        }

        public void Update()
        {
            Devices.AsParallel().ForAll(device => device.Value.GetState());
        }

        public Device this[string key]
        {
            get
            {
                return Devices[key];
            }
        }
    }
}
