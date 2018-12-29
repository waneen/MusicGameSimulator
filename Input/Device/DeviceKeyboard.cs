using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Input;

namespace MGS.Input
{
    public class Keyboard : Device
    {
        private KeyboardState KeyboardState { get; set; }

        public Keyboard(int DeviceIndex, string DeviceName) : base(DeviceIndex, DeviceName) { }

        public override void GetState() =>
            KeyboardState = OpenTK.Input.Keyboard.GetState(DeviceIndex);

        public override bool this[int ButtonIndex] =>
            KeyboardState[(short)ButtonIndex];

        public override List<int> GetPressedKeyIndex()
        {
            List<int> keys = new List<int>();
            for (int i = 0; i < 132; i++)
            {
                if (KeyboardState[(short)i]) keys.Add(i);
            }

            return keys;
        }
    }

    public struct Keycode
    {
        //全部追加して
        static readonly int X = (int)OpenTK.Input.Key.X;
        static readonly int D = (int)OpenTK.Input.Key.D;
        static readonly int C = (int)OpenTK.Input.Key.C;
        static readonly int F = (int)OpenTK.Input.Key.F;
        static readonly int V = (int)OpenTK.Input.Key.V;
        static readonly int G= (int)OpenTK.Input.Key.G;
        static readonly int B = (int)OpenTK.Input.Key.B;
        static readonly int LShift = (int)OpenTK.Input.Key.LShift;
    }
}
