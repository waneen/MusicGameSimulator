using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace MGS.Input
{
    public abstract class Device
    {
        public readonly int DeviceIndex;
        private readonly string DeviceName;
        public abstract void GetState();
        public abstract bool this[int ButtonIndex] { get; }
        public abstract List<int> GetPressedKeyIndex();

        public Device(int DeviceIndex,string DeviceName)
        {
            this.DeviceIndex = DeviceIndex;
            this.DeviceName = DeviceName;
        }
    }
}
