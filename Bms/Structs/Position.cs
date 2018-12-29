using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BMSLib
{
    public class Position:IComparable<Position>
    {
        public int Bar { get; set; }
        public double Pulse { get; set; }

        public Position(int Bar, double Pulse = 0)
        {
            this.Bar = Bar;
            this.Pulse = Pulse;
        }

        /// <summary>
        /// Positionの差分とBpmからミリ秒を計算します。
        /// </summary>
        /// <param name="Bpm">Bpm値</param>
        /// <returns>ミリ秒</returns>
        public long MilliSecond(double Bpm)
        {
            return (long)Math.Round(((double)Bar + Pulse) * 240 / Bpm * 1000);
        }

        public static Position operator -(Position x, Position y)
        {
            var Bar = x.Bar - y.Bar;
            var Pulse = x.Pulse - y.Pulse;

            return new Position(x.Bar - y.Bar, x.Pulse - y.Pulse);
        }

        public int CompareTo(Position other)
        {
            if (other == null) return 1;

            if (other.Bar == Bar)
                return Pulse.CompareTo(other.Pulse);

            return Bar.CompareTo(other.Bar);
        }
    }
}
