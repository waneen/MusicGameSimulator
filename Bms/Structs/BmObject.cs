using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSLib
{
    /// <summary>
    /// 可視ノーツやBGM・BGA・BPMなどのBMSオブジェクトの基底クラスです。
    /// </summary>
    public class BmObject
    {
        /// <summary>
        /// オブジェクトのチャンネルID
        /// </summary>
        public string Channel { get; set; } = "00";

        /// <summary>
        /// オブジェクトの位置
        /// </summary>
        public Position Position { get; set; } = new Position(0);

        /// <summary>
        /// オブジェクトの値
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// 曲開始時からの時間（ミリ秒）
        /// </summary>
        public long MilliSecond { get; set; }

        /// <summary>
        /// オブジェクトのデータ種類
        /// </summary>
        public DataType DataType { get; set; }

        /// <summary>
        /// オブジェクトが演奏可能か
        /// </summary>
        public bool Playable { get; set; }

        public BmObject(string Channel = "00", Position Position = null, string Value = "", DataType DataType = DataType.None, bool Playable = false)
        {
            this.Channel = Channel ?? "00";
            this.Position = Position ?? new Position(0);
            this.Value = Value;
            this.DataType = DataType;
            this.Playable = Playable;
        }

        /// <summary>
        /// BMSファイルから1行読み込んでパースします。
        /// </summary>
        /// <param name="Bar">小節数</param>
        /// <param name="Channel">オブジェクトのチャンネル</param>
        /// <param name="Value">偶数個の文字列からなるオブジェクトの位置情報を含む一行</param>
        /// <returns>解析後のオブジェクトリスト</returns>
        public static List<BmObject> Read(int Bar, string Channel, string Value)
        {
            if (Channel == "00")
            {
                throw new Exception("[(S)BmObject.Read(int Bar, string Channel, string Value)]パラメータ:Channelの値が不適切です。\nチャンネル00は使用できません。");
            }

            if (Channel == "02")
            {
                try
                {
                    return new List<BmObject>() { new BarlineObject(Bar, Value, Channel) };
                }
                catch
                {
                    throw new Exception("[(S)BmObject.Read(int Bar, string Channel, string Value)]パラメータ:Valueの値が不適切です。\n小節長倍率はdouble型の数のみ用いることが出来ます。");
                }
            }

            if (Value.Length == 0 || Value.Length % 2 != 0)
            {
                throw new Exception("[(S)BmObject.Read(int Bar, string Channel, string Value)]パラメータ:Valueの値が不適切です。\nオブジェクト定義の文字数が奇数もしくは0です。");
            }

            switch (Channel)
            {
                case "01":
                    return DevideValueLine<BgmObject>(Bar, Channel, Value);
                case "02":
                    return DevideValueLine<BarlineObject>(Bar, Channel, Value);
                case "03":
                case "08":
                    return DevideValueLine<BpmObject>(Bar, Channel, Value);
                case "04":
                case "06":
                case "07":
                    return DevideValueLine<BmpObject>(Bar, Channel, Value);
                case "09":
                    return DevideValueLine<StopObject>(Bar, Channel, Value);
                case "11":
                case "12":
                case "13":
                case "14":
                case "15":
                case "16":
                case "17":
                case "18":
                case "19":
                    return DevideValueLine<VisibleNoteObjectSP>(Bar, Channel, Value);
                case "21":
                case "22":
                case "23":
                case "24":
                case "25":
                case "26":
                case "27":
                case "28":
                case "29":
                    return DevideValueLine<VisibleNoteObjectDP>(Bar, Channel, Value);
                case "31":
                case "32":
                case "33":
                case "34":
                case "35":
                case "36":
                case "37":
                case "38":
                case "39":
                    return DevideValueLine<InvisibleNoteObjectSP>(Bar, Channel, Value);
                case "41":
                case "42":
                case "43":
                case "44":
                case "45":
                case "46":
                case "47":
                case "48":
                case "49":
                    return DevideValueLine<InvisibleNoteObjectDP>(Bar, Channel, Value);
                default:
                    return new List<BmObject>();
            }
        }

        /// <summary>
        /// 行を分割してBmObjectのリストを生成します。
        /// </summary>
        /// <typeparam name="T">BmObjectの派生型</typeparam>
        /// <param name="Bar">小節数</param>
        /// <param name="Channel">オブジェクトのチャンネル</param>
        /// <param name="Value">行の値</param>
        /// <returns></returns>
        private static List<BmObject> DevideValueLine<T>(int Bar, string Channel, string Value) where T : BmObject
        {
            var Objects = new List<BmObject>();
            for (int i = 0; i < Value.Length / 2; i++)
            {
                var constructorInfo = typeof(T).GetConstructor(new Type[] { typeof(Position), typeof(string), typeof(string) });
                var data = new String(new char[] { Value[2 * i], Value[2 * i + 1] });
                if (data == "00") continue;
                Objects.Add((T)constructorInfo.Invoke(new object[] { new Position(Bar, (double)i / (Value.Length / 2)), new String(new char[] { Value[2 * i], Value[2 * i + 1] }), Channel }));
            }

            return Objects;
        }
    }

    public enum DataType
    {
        None = 0,
        Sound,
        Image,
        Rhythm,
        Other
    }
}
