using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D19
{
    internal class Beacon
    {
        internal int X;
        internal int Y;
        internal int Z;

        internal Dictionary<double, Beacon> Neighbours = new Dictionary<double, Beacon>();
        internal double MaxDistance = 0;

        public Beacon Clone()
        {
            return new Beacon
            {
                X = this.X,
                Y = this.Y,
                Z = this.Z,
            };
        }
    }
}
