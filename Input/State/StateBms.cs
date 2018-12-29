using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MGS.Input
{
    public class StateBmsSP : State
    {
        public BmsKey Scratch { get; set; }
        public BmsKey Key1 { get; set; }
        public BmsKey Key2 { get; set; }
        public BmsKey Key3 { get; set; }
        public BmsKey Key4 { get; set; }
        public BmsKey Key5 { get; set; }
        public BmsKey Key6 { get; set; }
        public BmsKey Key7 { get; set; }
        //public GeneralKey Escape { get; set; }

        //その他適宜追加

        public StateBmsSP(DeviceHandler Devices) : base(Devices) { }
    }

    public class BmsKey:Key<BmsKeyState>
    {
        public override BmsKeyState State
        {
            get
            {
                return BmsKeyState.Down;
            }
        }
        public BmsKey(DeviceHandler Devices) : base(Devices) { }
    }

    public enum BmsKeyState
    {
        Up,
        Press,
        Down
    }
}
