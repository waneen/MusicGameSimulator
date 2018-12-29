using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;

namespace MGS.Skin
{
    public abstract class PartsDrawing
    {
        public List<Custom> CustomOptions { get; protected set; }
        public List<Bitmap> LoadedImages { get; private set; }
        public List<Lr2Font> Lr2Fonts { get; private set; }
        //以下はスクリプト化したい
        //public int StartInputMsec { get; private set; }
        //public int FadeOutMsec { get; private set; }

        public Timer Timer { get; private set; }

        public PartsDrawing()
        {
            //IMGとLr2Fontの読み込み
            //制御フローの読み込み(ex.if関連
            //TransColor
            //SRC/DST->スクリプトっぽく処理?Timerにかませてもいいかも
        }

        public abstract bool Draw();

        //protected bool DrawRectangle()
        //描画してくれるやつ
        //Timerに関係なく描画する感じ？

        //protected bool DrawNotes()
        //ノーツ描画してくれるやつ？
        //これはBMS再生時のみ
    }
}
