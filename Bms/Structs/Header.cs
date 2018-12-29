using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSLib
{
    public class Header
    {
        public int Player { get; set; } = 1;
        public int Rank { get; set; } = 2;
        public double Total { get; set; } = 160;
        public string StageFile { get; set; }
        public string Banner { get; set; }
        public string BackBmp { get; set; }
        public int? PlayLevel { get; set; }
        public int? Difficulty { get; set; }
        public string RawTitle { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string Artist { get; set; }
        public string SubArtist { get; set; }
        public string Genre { get; set; }
        //未定義時の動作を決定する
        public double InitBpm { get; set; } = 130;
        public string Email { get; set; }
        public string Url { get; set; }

        public Header() { }

        public bool Read(string command,string value)
        {
            switch (command.ToUpper())
            {
                case "PLAYER":
                    try
                    {
                        this.Player=int.Parse(value);
                        if (this.Player < 1 || this.Player > 5) throw new Exception($"[Header.Read(string command,string value]パラメータ:valueの値が不適切です。\n{command}の値を確認して下さい。");
                    }
                    catch
                    {
                        throw;
                    }
                    break;
                case "RANK":
                    try
                    {
                        this.Rank = int.Parse(value);
                        if(this.Rank<-1||this.Rank>4) throw new Exception($"[Header.Read(string command,string value]パラメータ:valueの値が不適切です。\n{command}の値を確認して下さい。");
                    }
                    catch
                    {
                        throw;
                    }
                    break;
                case "TOTAL":
                    try
                    {
                        this.Total = double.Parse(value);
                        if (this.Rank < -1 || this.Rank > 4) throw new Exception($"[Header.Read(string command,string value]パラメータ:valueの値が不適切です。\n{command}の値を確認して下さい。");
                    }
                    catch
                    {
                        throw;
                    }
                    break;
                case "StageFile":
                    this.StageFile = value;
                    break;
                case "Banner":
                    this.Banner = value;
                    break;
                case "BackBmp":
                    this.BackBmp = value;
                    break;
                case "TITLE":
                    this.RawTitle = value;
                    //TITLE&SubTitle insertion
                    break;
                case "SUBTITLE":
                    this.SubTitle = value;
                    break;
                case "ARTIST":
                    this.Artist = value;
                    break;
                case "SUBARTIST":
                    this.SubArtist = value;
                    break;
                case "GENRE":
                    this.Genre = value;
                    break;
                case "BPM":
                    try
                    {
                        this.InitBpm = double.Parse(value);
                    }
                    catch
                    {
                        throw new Exception($"[Header.Read(string command,string value]パラメータ:valueの値が不適切です。\n{command}の値を確認して下さい。");
                    }
                    break;
                case "EMAIL":
                    this.Email = value;
                    break;
                case "URL":
                    this.Url = value;
                    break;
                default:
                    return false;
            }

            return true;
        }
    }
}
