using System;
using System.Threading;
using MGS.Audio;
using MGS.Input;
using MGS.User;

namespace MGS.Info
{
    public class Info
    {
        //General
        public Timer Timer { get; set; }

        //UserConfigs
        public UserConfig UserConfig1P { get; set; }
        public UserConfig UserConfig2P { get; set; }
        //Status
        public bool IsConnectedIR { get; set; }
    }
}
