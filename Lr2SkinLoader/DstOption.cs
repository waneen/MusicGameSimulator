using System;
using System.Collections.Generic;
using System.Text;

namespace MGS.Skin.Lr2
{
    //ここでDstOption→スクリプトの変換をしたいのでそのためのenum定義

    #region General

    public enum Battle : int
    {
        Double_DoubleBattle = 10,
        Battle,
        Double_Battle_DoubleBattle,
        GhostBattle_Battle
    }

    public enum BgaSize : int
    {
        Normal = 30,
        Extend
    }

    public enum AutoPlay : int
    {
        Off = 32,
        On
    }

    public enum GhostType : int
    {
        Off = 34,
        A,
        B,
        C
    }

    public enum ScoreGraph : int
    {
        Off = 38,
        On
    }

    public enum BgaEnabled : int
    {
        Off = 40,
        On
    }

    //ノーマルゲージと赤ゲージって80％前後のこと？
    //42 1P側がノーマルゲージ
    //43 1P側が赤ゲージ
    //44 2P側がノーマルゲージ
    //45 2P側が赤ゲージ

    public enum DifficultyFilter : int
    {
        Enabled = 46,
        Disable
    }

    public enum InternetConnection : int
    {
        Online = 50,
        Offline
    }

    public enum ExtraMode : int
    {
        Off = 52,
        On
    }

    public enum AutoScratch1P : int
    {
        Off = 54,
        On
    }

    public enum AutoScratch2P : int
    {
        Off = 56,
        On
    }

    public enum Save : int
    {
        OnlyLamp = 60,
        Enabled,
        Disable
    }

    public enum Gauge : int
    {
        Easy = 63,
        Normal,
        Hard,
        Death,
        GAttack,
        PAttack
    }

    public enum LoadState : int
    {
        Loading = 80,
        Loaded
    }

    public enum ReplayState : int
    {
        Off = 82,
        Recording,
        Playing
    }

    public enum Result : int
    {
        Cleard = 90,
        Failed
    }

    #endregion General

    #region MusicSelect

    public enum ClearLamp : int
    {
        NotPlayed=100,
        Failed,
        Easy,
        Normal,
        Hard,
        FullCombo
    }

    public enum ScoreRate : int
    {
        AAA=110,
        AA,
        A,
        B,
        C,
        D,
        E,
        F
    }

    public enum CleardGauge:int
    {
        Normal=118,
        Hard,
        Hazard,
        Easy,
        PAttack,
        GAttack
    }

    public enum CleardPlaceOption : int
    {
        Original=126,
        Mirror,
        Random,
        SRandom,
        HRandom,
        AllScratch
    }

    public enum Difficulty : int
    {

    }

    #endregion MusicSelect
}
