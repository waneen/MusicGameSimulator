using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioLib;
using System.IO;
using System.Text.RegularExpressions;
using Sprache;

namespace BMSLib
{
    public class Bms
    {
        public string FileName { get; set; }
        public string DirectoryPath { get; set; }

        public Dictionary<int, string> ErrorLines { get; set; } = new Dictionary<int, string>();

        public Dictionary<string, string> WavDefinisitons { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, string> BmpDefinisitons { get; set; } = new Dictionary<string, string>();
        public Dictionary<string, double> BpmDefinisitons { get; set; } = new Dictionary<string, double>();

        public Header Header { get; set; } = new Header();
        public List<BmObject> RhythmObjects { get; set; } = new List<BmObject>();
        public List<BmObject> ChannelObjects { get; set; } = new List<BmObject>();

        public AudioPlayer AudioPlayer { get; set; }
        public long TimeLength { get; set; }

        public Bms()
        {

        }

        public void LoadAudio()
        {
            this.AudioPlayer = new AudioPlayer(WavDefinisitons);

            var max = this.ChannelObjects.Where(o => o.DataType == DataType.Sound)
                .Select(o => new { bmobject = o, Time = o.MilliSecond + this.AudioPlayer.AudioFiles[o.Value].Length })
                .OrderByDescending(o => o.Time)
                .First();

            this.TimeLength = max.Time;


        }

        public void PlaySound(int MilliSecond = 0)
        {
            foreach (var obj in ChannelObjects)
            {
                if (Math.Abs(obj.MilliSecond - MilliSecond) != 0) continue;
                var type = obj.GetType();
                if (type == typeof(VisibleNoteObjectSP) || type == typeof(BgmObject) || type == typeof(InvisibleNoteObjectSP))
                {
                    AudioPlayer.Play(obj.Value);
                }
            }
        }

        public void PlayAutoAsync(int MilliSecond = 0)
        {
            this.LoadAudio();

            var start = DateTime.Now;
            var now = DateTime.Now;
            var time = new TimeSpan(0, 0, 0, 0, (int)this.TimeLength);
            Console.WriteLine(time.ToString(@"mm\:ss"));

            while ((now - start).TotalMilliseconds < this.TimeLength)
            {
                if (now == DateTime.Now) continue;
                now = DateTime.Now;
                var sub = now - start;
                this.PlaySound((int)Math.Floor(sub.TotalMilliseconds));
            }
        }
    }
}
