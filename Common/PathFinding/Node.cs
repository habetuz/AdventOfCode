namespace AdventOfCode.Common.PathFinding
{
    using System;

    internal class Node : IComparable<Node>
    {
        internal int Cost { get; set; }

        internal int Distance { get; set; }

        internal int Priority { get => this.Cost + this.Distance; }

        internal bool Discovered { get; set; }

        internal int X { get; set; }

        internal int Y { get; set; }

        public override string ToString()
        {
            return $"{this.Priority} at x{this.X} y{this.Y}";
        }

        public int CompareTo(Node other)
        {
            return this.Priority - other.Priority;
        }

        internal static int GetDistance(Node a, Node b)
        {
            return Math.Abs(a.X - b.X) + Math.Abs(a.Y - b.Y);
        }
    }
}
