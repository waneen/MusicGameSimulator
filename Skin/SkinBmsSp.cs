using System;
using System.Collections.Generic;
using System.Text;

namespace MGS.Skin
{
    public class SkinBmsSP:Skin
    {
        //BMS


        public SkinBmsSP(string Title,string Composer,string Thumbnail) : base(Title, Composer, Thumbnail)
        {
            this.SceneType = SceneType._7keys;
        }
    }
}
