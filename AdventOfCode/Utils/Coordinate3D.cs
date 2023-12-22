using System.Diagnostics;

namespace AdventOfCode.Utils;

[DebuggerDisplay("{X}x{Y}x{Z}")]
public struct Coordinate3D
{
    public long X { get; set; }
    public long Y { get; set; }
    public long Z { get; set; }

    public Coordinate3D(long x, long y, long z) => (X, Y, Z) = (x, y, z);

    public static Coordinate3D operator +(Coordinate3D a, Coordinate3D b) =>
        new Coordinate3D(a.X + b.X, a.Y + b.Y, a.Z + b.Z);

    public static Coordinate3D operator -(Coordinate3D a, Coordinate3D b) =>
        new Coordinate3D(a.X - b.X, a.Y - b.Y, a.Z - b.Z);

    public static Coordinate3D operator +(Coordinate3D a, Direction3D b) => a + b.ToCoordinate();
    public static Coordinate3D operator -(Coordinate3D a, Direction3D b) => a - b.ToCoordinate();

    public static bool operator ==(Coordinate3D a, Coordinate3D b)
    {
        return a.X == b.X && a.Y == b.Y && a.Z == b.Z;
    }

    public static bool operator !=(Coordinate3D a, Coordinate3D b)
    {
        return !(a == b);
    }

    public override bool Equals(object? obj)
    {
        return obj is Coordinate3D other && this == other;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(X, Y, Z);
    }
}
