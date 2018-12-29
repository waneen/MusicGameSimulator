using System;
using MGS.Input;
using MGS.Audio;
using MGS.Skin;
using System.Timers;

namespace MGS.Scene
{
    public class SceneBmsSP : Scene
    {
        public StateBmsSP State { get; set; }
        public SkinBmsSP Skin { get; set; }

        public SceneBmsSP(DeviceHandler Devices, SkinBmsSP SkinBmsSp, AudioHandler AudioHandler, Timer Timer) : base(Devices, AudioHandler, Timer)
        {
            this.State = new StateBmsSP(Devices);
            this.Skin = SkinBmsSp;
        }

        //DrawとUpdateは別にオーバーライドする必要ない？

        public override void Draw()
        {
            Skin.Draw();
        }

        public override void Sound()
        {
            
        }

        public override void Update()
        {
            Devices.Update();
        }
    }
}
