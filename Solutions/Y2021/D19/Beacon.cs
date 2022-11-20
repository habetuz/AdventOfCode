namespace AdventOfCode.Solutions.Y2021.D19
{
    using System.Collections.Generic;

    internal class Beacon
    {
        internal Beacon()
        {
            this.Neighbours = new Dictionary<double, Beacon>();
            this.MaxDistance = 0;
        }

        internal int X { get; set; }

        internal int Y { get; set; }

        internal int Z { get; set; }

        internal Dictionary<double, Beacon> Neighbours { get; set; }

        internal double MaxDistance { get; set; }

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
