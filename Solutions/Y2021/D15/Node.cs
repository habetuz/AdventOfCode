using SharpLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Solutions.Y2021.D15
{
    internal class Node : IComparable<Node>
    {
        internal int RiskLevel { get; }

        internal int X { get; }

        internal int Y { get; }

        internal int F { get; set; } = 0;

        internal bool Discovered { get; set; } = false;

        internal Node(int riskLevel, int x, int y)
        {
            RiskLevel = riskLevel;
            X = x;
            Y = y;
        }

        public int CompareTo(Node other)
        {
            return this.F - other.F;
        }
    }
}
