using System;
using MGS.Input;

namespace MGS.User
{
    public class UserConfig
    {
        //General
        public bool NetworkEnabled { get; set; }
        public bool FullScreenEnabled { get; set; }
        public SaveReplay SaveReplay { get; set; }
        public GhostType GhostType { get; set; }
        public Battle Battle { get; set; }
        public Gauge Gauge { get; set; }
        //BGA
        public bool BgaEnabled { get; set; }
        //Pitch
        public bool PitchChangeEnabled { get; set; }
        public int Pitch { get; set; }
        //Judge
        public int HighSpeed { get; set; }
        public HighSpeedFix HighSpeedFix { get; set; }
        public int JudgeTiming { get; set; }
        public bool AutoTimingAdjusts { get; set; }
        //NotesBehavior
        public RandomOption RandomOption { get; set; }
        public bool Flips { get; set; }
        public bool IsScratchAssisted { get; set; }
        //LaneEffect
        public LaneHead LaneHead { get; set; }
        public int LaneHeadValue { get; set; }
        public LaneFoot LaneFoot { get; set; }
        public int LaneFootValue { get; set; }
        //Score
        public int DefaultTarget { get; set; }
        public bool ScoreGraphEnabled { get; set; }
    }

    public enum BgaSize
    {
        Normal,
        Extend
    }

    public enum GhostType
    {
        Off,
        A,
        B,
        C
    }

    public enum Gauge
    {
        AssistedEasy,
        Easy,
        Normal,
        Hard,
        ExHard,
        Hazard,
        PAttack,
        GAttack
    }

    public enum RandomOption
    {
        Original,
        Mirror,
        Random,
        RRandom,
        RandomMirror,
        RRandomMirror,
        SRandom,
        HRandom,
        AllScratch
    }

    public enum HighSpeedFix
    {
        OFF,
        MaxBpm,
        MinBpm,
        Average,
        Constant
    }

    public enum Battle
    {
        Off,
        Versus,
        Ghost,
        Double,
        Online
    }

    public enum SaveReplay
    {
        Off,
        All,
        ScoreUpdate
    }

    public enum LaneHead
    {
        Off,
        Sudden,
        SuddenPlus
    }

    public enum LaneFoot
    {
        Off,
        Hidden,
        HiddenPlus,
        Lift
    }
}
