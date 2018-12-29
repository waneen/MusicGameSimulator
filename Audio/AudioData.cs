using System;
using System.Collections.Generic;
using System.Text;
using OpenTK.Audio.OpenAL;

namespace MGS.Audio
{
    public class AudioData
    {
        public string Key { get; private set; }
        public AudioFile SoundClip { get; private set; }

        int Buffer = 0;
        int Source = -1;
        public ALSourceState SourceState { get; private set; }

        public AudioData(string Key,string FilePath)
        {
            this.Key = Key;
            this.SoundClip = SoundClip.Load(FilePath);
        }

        //Errorが出たとき例外をスロー
        public void Open()
        {
            AL.GenBuffers(1, out Buffer);
            Console.WriteLine(AL.GetError().ToString());
            AL.BufferData(Buffer, SoundClip.IsMonoral ? ALFormat.Mono16 : ALFormat.Stereo16, SoundClip.Samples, SoundClip.Samples.Length * sizeof(short), SoundClip.SampleRate);
        }

        public void Close()
        {
            AL.SourceStop(Source);
            AL.DeleteSource(Source);
            AL.DeleteBuffer(Buffer);
        }

        public void Play(int Source)
        {
            Stop();
            this.Source = Source;
            AL.Source(Source, ALSourcei.Buffer, Buffer);
            AL.SourcePlay(Source);
            SourceState = AL.GetSourceState(Source);
        }

        public void Pause()
        {
            AL.SourcePause(Source);
            SourceState = AL.GetSourceState(Source);
        }

        public void Stop()
        {
            AL.SourceStop(Source);
            SourceState = AL.GetSourceState(Source);
        }
    }
}
