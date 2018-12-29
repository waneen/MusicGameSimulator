using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGS.Input
{
    public abstract class State
    {
        protected DeviceHandler Devices { get; set; }

        public State(DeviceHandler Devices)
        {
            this.Devices = Devices;
        }
    }

    public abstract class Key<T>where T:Enum
    {
        protected DeviceHandler Devices { get; set; }

        public abstract T State { get; }

        protected List<bool> OldKeyPress { get; set; }

        protected List<bool> KeyPress
        {
            get
            {
                return KeyAssign.Select(kvp => Devices[kvp.Key][kvp.Value]).ToList();
            }
        }
        
        //ここだけJsonパース対象にする
        public Dictionary<string, int> KeyAssign { get; private set; }

        public Key(DeviceHandler Devices)
        {
            this.Devices = Devices;
        }
    }
}
