using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSLib
{
    public class BarlineObject : BmObject
    {
        public double LengthRatio { get; set; }

        public BarlineObject(int Bar, string Value, string Channel) : base(Channel, new Position(Bar), Value, DataType.Rhythm, false)
        {
            try
            {
                this.LengthRatio = double.Parse(Value);
            }
            catch
            {
                throw;
            }
        }
    }
}
