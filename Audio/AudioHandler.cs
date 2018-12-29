using System;
using System.Collections.Generic;
using System.Linq;
using OpenTK.Audio.OpenAL;
using OpenTK;
using System.Threading.Tasks;

namespace MGS.Audio
{
    public class AudioHandler
    {
        public Dictionary<string, AudioData> AudioData { get; set; }
        public List<string> Devices { get; private set; }

        ContextHandle AudioContext;
        IntPtr Device;
        int[] Sources;
        int count = 0;
        int MaxSourceCount;

        public AudioHandler(Dictionary<string, string> WavDefinitions,string DeviceName=null)
        {
            Devices = Alc.GetString((IntPtr)null, AlcGetStringList.AllDevicesSpecifier).ToList();
            ChangeAudioDevice(DeviceName);
            this.AudioData = WavDefinitions.AsParallel().ToDictionary(x => x.Key, x => new AudioData(x.Key, x.Value));
        }


        ~AudioHandler()
        {
            Alc.MakeContextCurrent(ContextHandle.Zero);
            Alc.DestroyContext(AudioContext);
            Alc.CloseDevice(Device);
        }

        public void ChangeAudioDevice(string DeviceName)
        {
            this.Device = Alc.OpenDevice(DeviceName);

            this.AudioContext = Alc.CreateContext(Device, (int[])null);
            Alc.MakeContextCurrent(AudioContext);

            Alc.GetInteger(Device, AlcGetInteger.AttributesSize, 1, out int size);
            int[] data = new int[size];
            Alc.GetInteger(Device, AlcGetInteger.AllAttributes, size, data);
            this.MaxSourceCount = data[Alc.GetEnumValue(Device, "ALC_MONO_SOURCES")];//ここあやしい

            Sources = AL.GenSources(MaxSourceCount);
        }

        public void StopAll()
        {
            //全部止める
        }

        public void Play(string Key)
        {
            //非同期いる？
            Task.Run(() =>
            {
                if (Key == "00") return;
                try
                {
                    //ここ無限ループ
                    while (AL.GetSourceState(Sources[count]) == ALSourceState.Playing)
                    {
                        count = (count + 1) % MaxSourceCount;
                    }

                    AudioData[Key].Play(Sources[count]);
                    count = (count + 1) % MaxSourceCount;
                }
                catch
                {
                    throw;
                }
            });
        }
    }
}
