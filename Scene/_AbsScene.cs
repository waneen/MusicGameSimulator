using System;
using System.Collections.Generic;
using System.Text;
using MGS.Input;
using MGS.Audio;
using MGS.Skin;
using System.Timers;

namespace MGS.Scene
{
    public abstract class Scene
    {
        public AudioHandler AudioHandler { get; private set; }
        public DeviceHandler Devices { get; private set; }
        public Timer Timer { get; set; }

        public Scene(DeviceHandler Devices,AudioHandler AudioHandler,Timer Timer)
        {
            this.Devices = Devices;
            this.AudioHandler = AudioHandler;
            this.Timer = Timer;
        }

        public abstract void Draw();
        public abstract void Sound();
        public abstract void Update();

    }
}
