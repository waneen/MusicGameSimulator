using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BMSLib
{
    static public class BmsParser
    {
        //bms ClassにErrorLinesを入れてコマンドパース関数をインスタンスメソッドにする
        static public Bms Parse(string filePath)
        {
            var bms = new Bms();

            if (!(filePath.EndsWith(".bms") || filePath.EndsWith(".bme") || filePath.EndsWith(".bml")))
                throw new ArgumentException("拡張子が不正です。");

            if (!File.Exists(filePath))
                throw new ArgumentException("ファイルが存在しません。");

            bms.DirectoryPath = Path.GetDirectoryName(filePath);
            bms.FileName = Path.GetFileName(filePath);

            return bms.ReadLine().ParseFlow().ParseHeaderInfo().ParseRhythmChannel().ParseChannel();
        }

        /// <summary>
        /// ファイルからテキストを読み込みます。
        /// その際にコマンド以外の行の削除、#の頭出しを行います。
        /// </summary>
        /// <param name="FilePath">読み込むファイルパス</param>
        /// <returns>処理された行ごとの文字列のリスト</returns>
        static private Bms ReadLine(this Bms bms)
        {
            using (StreamReader sr = new StreamReader(bms.FileName, Encoding.GetEncoding("Shift_JIS"), false))
            {
                int count = 1;
                while (sr.Peek() > -1)
                {
                    var Line = sr.ReadLine();
                    for (int i = 0; i < Line.Length; i++)
                    {
                        if (Line[i] == '#' || Line[i] == '%')
                        {
                            bms.ErrorLines.Add(count, Line.Substring(i));
                            break;
                        }
                        if (Line[i] == ' ' || Line[i] == '　' || Line[i] == '\t')
                        {
                            continue;
                        }
                    }

                    count++;
                }
            }

            return bms;
        }

        /// <summary>
        /// コマンドフローを解析して処理します。
        /// </summary>
        /// <param name="Lines">頭出しされたテキスト</param>
        /// <returns>コマンドフロー処理後の文字列のリスト</returns>
        //ToDo:DEFの位置ずれによる残りの処理
        //     randomの未定義など
        static private Bms ParseFlow(this Bms bms)
        {
            #region Regexes
            Regex triggerRegex = new Regex(@"#(?<Command>RANDOM|SWITCH) (?<Value>[\d]*)$");
            //Regex setRegex = new Regex(@"#(?<Command>RANDOM|SWITCH) (?<Value>[\d]*)$");
            Regex branchRegex = new Regex(@"#(?<Command>IF|CASE) (?<Value>[\d]*)$");
            Regex defaultRegex = new Regex(@"#(?<Command>DEF)$");
            Regex backRegex = new Regex(@"#(?<Command>ENDIF|SKIP)");
            Regex endRegex = new Regex(@"#(?<Command>ENDRANDOM|ENDSW)");
            #endregion Regexes

            #region Variables
            //フローで生成された乱数のスタック
            List<int> DigitStack = new List<int> { };
            //入れ子の深さ
            //0ならフローなし
            int nestDepth = 0;
            //処理モード
            ParseMode mode = ParseMode.Read;
            string ObserveKey = "";
            bool defaultFlag = true;
            #endregion Variables

            foreach (var line in bms.ErrorLines)
            {
                var upper = line.Value.ToUpper();

                #region Trigger
                Match trigger = triggerRegex.Match(upper);
                if (trigger.Success && mode != ParseMode.Ignore)
                {
                    if (mode == ParseMode.Read)
                        nestDepth++;
                    else
                        DigitStack.RemoveAt(DigitStack.Count - 1);
                    mode = ParseMode.Observe;
                    ObserveKey = trigger.Groups["Command"].Value;

                    int n;
                    try
                    {
                        n = int.Parse(trigger.Groups["Value"].Value);
                    }
                    catch
                    {
                        throw new Exception("nの値が不正です。乱数生成に用いることのできる数値は整数型のみです。");
                    }
                    if (n < 1) throw new Exception("nの値が不正です。０または負の整数から乱数を生成することはできません。");

                    Random rand = new Random();
                    DigitStack.Add(rand.Next(n) + 1);
                    bms.ErrorLines.Remove(line.Key);
                    continue;
                }
                #endregion Trigger

                #region Branch
                Match branch = branchRegex.Match(upper);
                if (branch.Success && mode == ParseMode.Observe)
                {
                    var key = branch.Groups["Command"].Value == "IF" ? "RANDOM" : "SWITCH";
                    if (ObserveKey == key)
                    {

                        int k;
                        try
                        {
                            k = int.Parse(branch.Groups["Value"].Value);
                        }
                        catch
                        {
                            throw new Exception("kの値が不正です。分岐に使える数値は整数型のみです。");
                        }
                        if (k < 1) throw new Exception("kの値が不正です。０または負の整数を指定することはできません。");

                        mode = DigitStack[nestDepth - 1] == k ? ParseMode.Read : ParseMode.Ignore;
                        defaultFlag = false;
                    }
                    bms.ErrorLines.Remove(line.Key);
                    continue;
                }
                #endregion Branch

                #region Default
                Match def = defaultRegex.Match(upper);
                if (def.Success && mode == ParseMode.Observe && ObserveKey == "SWITCH" && defaultFlag)
                {
                    mode = ParseMode.Read;
                    bms.ErrorLines.Remove(line.Key);
                    continue;
                }
                #endregion Default

                #region Back
                Match back = backRegex.Match(upper);
                if (back.Success && mode != ParseMode.Observe)
                {
                    var key = branch.Groups["Command"].Value == "SKIP" ? "SWITCH" : "RANDOM";
                    if (ObserveKey == key)
                    {
                        mode = ParseMode.Observe;
                    }
                    bms.ErrorLines.Remove(line.Key);
                    continue;
                }
                #endregion Back

                #region End
                Match end = endRegex.Match(upper);
                if (end.Success && nestDepth > 0)
                {
                    mode = ParseMode.Read;
                    nestDepth--;
                    DigitStack.RemoveAt(DigitStack.Count - 1);
                    bms.ErrorLines.Remove(line.Key);
                    continue;
                }
                #endregion End
            }

            return bms;
        }

        /// <summary>
        /// ヘッダー及び定義をを読み込みます。
        /// </summary>
        /// <param name="Lines">コマンドフロー処理後の文字列のリスト</param>
        /// <param name="bms">bmsインスタンス</param>
        /// <returns>処理されなかった文字列のリスト</returns>
        static private Bms ParseHeaderInfo(this Bms bms)
        {
            Regex infoRegex = new Regex(@"[#%](?<command>.*?) (?<value>.*)");
            Regex definitionRegex = new Regex("^#(?<type>WAV|BMP|BPM)(?<key>[0-9A-Za-z]{2}) (?<value>.*)");

            foreach (var line in bms.ErrorLines)
            {
                Match definition = definitionRegex.Match(line.Value);
                if (definition.Success)
                {
                    switch (definition.Groups["type"].Value)
                    {
                        case "WAV":
                            try
                            {
                                bms.WavDefinisitons.Add(definition.Groups["key"].Value, Path.Combine(new string[] { bms.DirectoryPath, definition.Groups["value"].Value }));
                            }
                            catch
                            {
                                throw;
                            }
                            bms.ErrorLines.Remove(line.Key);
                            continue;
                        case "BMP":
                            try
                            {
                                bms.BmpDefinisitons.Add(definition.Groups["key"].Value, Path.Combine(new string[] { bms.DirectoryPath, definition.Groups["value"].Value }));
                            }
                            catch
                            {
                                throw;
                            }
                            bms.ErrorLines.Remove(line.Key);
                            continue;
                        case "BPM":
                            try
                            {
                                double value = double.Parse(definition.Groups["value"].Value);
                                bms.BpmDefinisitons.Add(definition.Groups["key"].Value, value);
                            }
                            catch
                            {
                                throw;
                            }
                            bms.ErrorLines.Remove(line.Key);
                            continue;
                    }
                }

                Match info = infoRegex.Match(line.Value);
                if (info.Success)
                {
                    bms.Header.Read(info.Groups["command"].Value, info.Groups["value"].Value);
                    bms.ErrorLines.Remove(line.Key);
                    continue;
                }
            }

            return bms;
        }

        /// <summary>
        /// リズムや小節に関するチャンネル情報を読み込みます
        /// ToDO:同時の場合の順
        /// </summary>
        /// <param name="Lines"></param>
        /// <param name="bms"></param>
        /// <returns></returns>
        static private Bms ParseRhythmChannel(this Bms bms)
        {
            Regex rhythmRegex = new Regex(@"#(?<bar>[\d]{3})(?<channel>02|03|08|09):(?<value>[0-9a-zA-Z\.]*)");

            bms.RhythmObjects.Add(BpmObject.InitBpmObject(bms.Header.InitBpm));

            foreach (var line in bms.ErrorLines)
            {
                Match rhythm = rhythmRegex.Match(line.Value);
                if (rhythm.Success)
                {
                    try
                    {
                        var bar = int.Parse(rhythm.Groups["bar"].Value);
                        var channel = rhythm.Groups["channel"].Value;
                        string value = rhythm.Groups["value"].Value;
                        var bmobject = BmObject.Read(bar, channel, value);
                        bms.RhythmObjects.AddRange(bmobject);
                    }
                    catch
                    {
                        throw new Exception($"{line.Key}:{line.Value}");
                        throw;
                    }
                    bms.ErrorLines.Remove(line.Key);
                    continue;
                }
            }

            bms.RhythmObjects=bms.RhythmObjects
                /*.Select(r =>
            {
                if (r.Channel != "08") return r;
                var ch8 = (BpmObject)r;
                ch8.BpmValue = bms.BpmDefinisitons[ch8.Value];
                return ch8;
            })*/
            .OrderBy(r => r.Position).ToList();

            return bms;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Lines"></param>
        /// <param name="bms"></param>
        /// <returns></returns>
        static private Bms ParseChannel(this Bms bms)
        {
            Regex channelRegex = new Regex("#(?<bar>[0-9]{3})(?<channel>[0-9A-Za-z]{2}):(?<value>[0-9a-zA-Z]*)");

            foreach (var line in bms.ErrorLines)
            {
                Match channel = channelRegex.Match(line.Value);
                if (channel.Success)
                {
                    if (channel.Success)
                    {
                        try
                        {
                            var bar = int.Parse(channel.Groups["bar"].Value);
                            var ch = channel.Groups["channel"].Value;
                            var value = channel.Groups["value"].Value;
                            var bmobject = BmObject.Read(bar, ch, value);

                            foreach (var b in bmobject)
                            {
                                bms.ChannelObjects.Add(SetMilliSecond(b, bms.RhythmObjects));
                            }
                        }
                        catch
                        {
                            throw;
                        }
                        bms.ErrorLines.Remove(line.Key);
                        continue;
                    }
                }
            }

            return bms;
        }

        /// <summary>
        /// bmObjectに対してミリ秒を計算します。
        /// </summary>
        /// <param name="bmObject"></param>
        /// <param name="RhythmObjects"></param>
        /// <returns></returns>
        static private BmObject SetMilliSecond(BmObject bmObject, List<BmObject> RhythmObjects)
        {
            List<BarlineObject> BarlineObjects = RhythmObjects.OfType<BarlineObject>().ToList();

            for (int i = 0; i < RhythmObjects.Count; i++)
            {
                BpmObject nowObject = (BpmObject)RhythmObjects.First();
                BpmObject recentObject = nowObject;

                //Bpmから計算
                if (bmObject.Position.CompareTo(RhythmObjects[i].Position) > 0)
                {
                    var sub = bmObject.Position - recentObject.Position;

                    foreach (var barline in BarlineObjects)
                    {
                        if (recentObject.Position.Bar <= barline.Position.Bar && bmObject.Position.Bar > barline.Position.Bar)
                        {
                            sub.Pulse -= (1 - barline.LengthRatio);
                        }
                    }

                    bmObject.MilliSecond += sub.MilliSecond(nowObject.BpmValue);
                    break;
                }


                //Bpm変更
                if (RhythmObjects[i].GetType() == typeof(BpmObject))
                {
                    nowObject = (BpmObject)RhythmObjects[i];
                    var sub = nowObject.Position - recentObject.Position;

                    foreach (var barline in BarlineObjects)
                    {
                        if (recentObject.Position.Bar <= barline.Position.Bar && nowObject.Position.Bar > barline.Position.Bar)
                        {
                            sub.Pulse -= (1 - barline.LengthRatio);
                        }
                    }

                    bmObject.MilliSecond += sub.MilliSecond(nowObject.BpmValue);
                    recentObject = nowObject;
                    continue;
                }

                //停止
                if (RhythmObjects[i].GetType() == typeof(StopObject))
                {
                    StopObject stopObject = (StopObject)RhythmObjects[i];
                    var duration = new Position(0, stopObject.stopDuration);
                    bmObject.MilliSecond += duration.MilliSecond(nowObject.BpmValue);
                    continue;
                }
            }

            Console.WriteLine($"{bmObject.Position.Bar}:{bmObject.MilliSecond}");
            return bmObject;
        }

        enum ParseMode
        {
            Read,
            Observe,
            //Finalize,
            Ignore,
        }
    }
}
