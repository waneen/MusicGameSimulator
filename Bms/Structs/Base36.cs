using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSLib.Structs
{
    public class Base36
    {
        static readonly string ValueMap = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";

        public static int StringToInt(string key)
        {
            key = key.ToUpper();
            return ValueMap.IndexOf(key[0]) + ValueMap.IndexOf(key[1]);
        }
    }
}
