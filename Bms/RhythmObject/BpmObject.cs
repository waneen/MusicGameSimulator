using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSLib
{
    class BpmObject : BmObject
    {
        public double BpmValue { get; set; }

        public BpmObject(Position Position, string Value, string Channel) : base(Channel, Position, Value, DataType.Rhythm, false)
        {
            try
            {
                switch (Channel)
                {
                    case "03":
                        this.BpmValue = Base16ToInt(Value);
                        break;
                    case "08":
                        this.BpmValue = double.Parse(Value);
                        break;
                }
            }
            catch
            {
                throw;
            }
        }

        public BpmObject(Position Position, string Value) : this(Position, Value, "03")
        {

        }

        static public BpmObject InitBpmObject(double BpmValue)
        {
            return new BpmObject(new Position(0, 0), BpmValue.ToString(), "08");
        }

        static readonly string ValueMap = "0123456789ABCDEF";

        private static int Base16ToInt(string key)
        {
            key = key.ToUpper();
            return ValueMap.IndexOf(key[0])*16 + ValueMap.IndexOf(key[1]);
        }
    }
}
