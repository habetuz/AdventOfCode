namespace AdventOfCode.Common.Space
{
    internal struct Coordinate2D
    {
        internal Coordinate2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        internal Coordinate2D(string coordinate)
        {
            var values = coordinate.Split(',');
            this.X = int.Parse(values[0]);
            this.Y = int.Parse(values[1]);
        }

        internal int X { get; set; }

        internal int Y { get; set; }

        public static bool operator ==(Coordinate2D left, Coordinate2D right) => left.X == right.X && left.Y == right.Y;

        public static bool operator !=(Coordinate2D left, Coordinate2D right) => left.X != right.X || left.Y != right.Y;

        public override string ToString()
        {
            return this.X + "," + this.Y;
        }
    }
}
