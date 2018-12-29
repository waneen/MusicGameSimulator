using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSLib
{
    public class WavFile
    {
        public string FilePath { get; set; }
        //OpenALのWavの何か
        //public Wav wav { get; set; }

        public WavFile(string filePath)
        {
            this.FilePath = filePath;
        }
    }
}
