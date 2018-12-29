using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace MGS.Audio
{
    class Wav : AudioFile
    {
        //RIFF
        string RiffHeader;
        int FileSize;
        string WaveHeader;
        //Fmt
        string FormatChunk;
        int FormatChunkSize;
        int FormatId;
        int BytePerSec;
        int BlockSize;
        int BitPerSample;
        //Data
        string DataChunk;
        int DataChunkSize;
        byte[] RawData;
        
        protected override AudioFile Read(string Filepath)
        {
            var wav = new Wav();

            using (FileStream fs = new FileStream(Filepath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                try
                {
                    BinaryReader br = new BinaryReader(fs);
                    wav.RiffHeader = Encoding.GetEncoding(20127).GetString(br.ReadBytes(4));
                    wav.FileSize = BitConverter.ToInt32(br.ReadBytes(4), 0);
                    wav.WaveHeader = Encoding.GetEncoding(20127).GetString(br.ReadBytes(4));

                    bool readFmtChunk = false;
                    bool readDataChunk = false;
                    while (!readFmtChunk || !readDataChunk)
                    {
                        // ChunkIDを取得する
                        string chunk = Encoding.GetEncoding(20127).GetString(br.ReadBytes(4));

                        if (chunk.ToLower().CompareTo("fmt ") == 0)
                        {
                            // fmtチャンクの読み込み
                            wav.FormatChunk = chunk;
                            wav.FormatChunkSize = BitConverter.ToInt32(br.ReadBytes(4), 0);
                            wav.FormatId = BitConverter.ToInt16(br.ReadBytes(2), 0);
                            wav.Channel = BitConverter.ToInt16(br.ReadBytes(2), 0);
                            wav.SampleRate = BitConverter.ToInt32(br.ReadBytes(4), 0);
                            wav.BytePerSec = BitConverter.ToInt32(br.ReadBytes(4), 0);
                            wav.BlockSize = BitConverter.ToInt16(br.ReadBytes(2), 0);
                            wav.BitPerSample = BitConverter.ToInt16(br.ReadBytes(2), 0);

                            readFmtChunk = true;
                        }
                        else if (chunk.ToLower().CompareTo("data") == 0)
                        {
                            // dataチャンクの読み込み
                            wav.DataChunk = chunk;
                            wav.DataChunkSize = BitConverter.ToInt32(br.ReadBytes(4), 0);

                            wav.RawData = br.ReadBytes(wav.DataChunkSize);
                            wav.Samples = new short[wav.RawData.Length];
                            Parallel.For(0, wav.RawData.Length,
                                i =>
                                {
                                    wav.Samples[i] = (short)(wav.RawData[i] * 256);
                                });

                            // 再生時間を算出する
                            wav.Length = (int)(((double)wav.DataChunkSize / (double)(wav.SampleRate * wav.BlockSize)) * 1000);
                        }
                        else
                        {
                            // 不要なチャンクの読み捨て
                            Int32 size = BitConverter.ToInt32(br.ReadBytes(4), 0);
                            if (0 < size)
                            {
                                br.ReadBytes(size);
                            }
                        }
                    }
                }
                catch
                {
                    throw;
                }

                return wav;
            }
        }
    }
}
