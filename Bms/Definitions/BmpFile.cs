using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSLib
{
    public class BmpFile
    {
        public string FilePath { get; set; }
        //Bmpファイル本体
        //public Bmp Bmp { get; set; }

        public BmpFile(string filePath)
        {
            this.FilePath = filePath;
        }
    }
}
