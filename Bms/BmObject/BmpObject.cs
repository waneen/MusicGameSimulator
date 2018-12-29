using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSLib
{
    class BmpObject : BmObject
    {
        //public WavFile WavFile { get; set; } = null;

        public BmpObject(Position Position, string Value, string Channel) : base(Channel, Position, Value, DataType.Image, false) { }
    }
}
