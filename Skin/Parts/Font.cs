using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace MGS.Skin
{
    public class Lr2Font
    {
        private static readonly Encoding sjisEnc = Encoding.GetEncoding("Shift_JIS");
        public string FilePath { get; private set; }
        public int Size { get; set; }
        public int Measure { get; set; } = 0;
        private List<Bitmap> TextImages { get; set; }
        private Dictionary<int, Bitmap> Charactors { get; set; }

        public Lr2Font(string FilePath)
        {
            this.FilePath = FilePath;
            //Text読み込み
            //SizeMeasureの読み込み
            //Imagesの読み込み
            //ImagesからCharactorsの生成
        }

        public Bitmap this[string str]
        {
            get
            {
                //Charactorsから文字列の画像を生成して返す
                return null;
            }
        }
    }
}
