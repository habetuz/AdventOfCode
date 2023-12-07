using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Utils
{
    public struct Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x, int y) => (X, Y) = (x, y);

        public static Coordinate operator +(Coordinate a, Coordinate b) => new Coordinate(a.X + b.X, a.Y + b.Y);
        public static Coordinate operator -(Coordinate a, Coordinate b) => new Coordinate(a.X - b.X, a.Y - b.Y);
        public static Coordinate operator *(Coordinate a, int b) => new Coordinate(a.X * b, a.Y * b);
        public static Coordinate operator /(Coordinate a, int b) => new Coordinate(a.X / b, a.Y / b);

        public static explicit operator Coordinate((int, int) coordinate) {
            return new Coordinate(coordinate.Item1, coordinate.Item2);
        }

        public static implicit operator (int, int)(Coordinate coordinate) {
            return (coordinate.X, coordinate.Y);
        }

        public static bool operator ==(Coordinate a, Coordinate b) => a.X == b.X && a.Y == b.Y;
        public static bool operator !=(Coordinate a, Coordinate b) => a.X != b.X || a.Y != b.Y;

        public override bool Equals(object? obj) => obj is Coordinate coordinate && this == coordinate;
        public override int GetHashCode() => HashCode.Combine(X, Y);

        public override string ToString() => $"({X}, {Y})";
    }
}