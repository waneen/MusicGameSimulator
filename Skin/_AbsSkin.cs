using System;
using System.Collections.Generic;
using System.Text;

namespace MGS.Skin
{
    public abstract class Skin
    {
        public SceneType SceneType { get; protected set; }
        public string Title { get; protected set; } = "";
        public string Composer { get; protected set; } = "";
        public string Thumbnail { get; protected set; } = null;

        public List<Custom> CustomOptions { get; protected set; }
        //IMG List
        //Font List


        public Skin(string Title,string Composer,string Thumbnail)
        {
            this.Title = Title;
            this.Composer = Composer;
            this.Thumbnail = Thumbnail;
        }

        public abstract bool Draw();
    }
    
    public enum SceneType : int
    {
        ERROR = -1,
        _7keys = 0,
        _5keys,//対応したくない
        _14keys,
        _10keys,//対応したくない
        _9keys,
        MusicSelect,
        Decide,
        Result,//何のリザルトかを分けるべきでは？
        KeyConfig,
        SkinSelect,
        SoundSet,
        Theme,
        _5KeysBattle,//対応したくない
        _7KeysBattle = 13,
        CourseEdit,
        CourseResult,
        _9KeysBattle,//うんこ
        Opening,
        ModeSelect,
        ModeDecide,
        CourseSelect
    }
}
