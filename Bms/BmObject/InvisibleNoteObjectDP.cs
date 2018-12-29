using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSLib
{
    class InvisibleNoteObjectDP:BmObject
    {
        //public WavFile WavFile { get; set; } = null;

        public InvisibleNoteObjectDP(Position Position, string Value, string Channel) : base(Channel, Position, Value, DataType.Sound, false) { }
    }
}
