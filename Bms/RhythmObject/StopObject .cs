using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSLib
{
    class StopObject : BmObject
    {
        public double stopDuration { get; set; } = 0;

        public StopObject(Position Position, string Value, string Channel) : base(Channel, Position, Value, DataType.Rhythm, false)
        {

        }
    }
}
