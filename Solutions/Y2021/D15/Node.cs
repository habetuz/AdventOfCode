// <copyright file="Node.cs" company="Marvin Fuchs">

namespace AdventOfCode.Solutions.Y2021.D15
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using SharpLog;

    internal class Node : IComparable<Node>
    {
        internal Node(int riskLevel, int x, int y)
        {
            this.RiskLevel = riskLevel;
            this.X = x;
            this.Y = y;
        }

        internal int RiskLevel { get; }

        internal int X { get; }

        internal int Y { get; }

        internal int F { get; set; } = 0;

        internal bool Discovered { get; set; } = false;

        public int CompareTo(Node other)
        {
            return this.F - other.F;
        }
    }
}
