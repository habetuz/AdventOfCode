using System.Diagnostics;

namespace AdventOfCode.Utils;

[DebuggerDisplay(
    "{Position} => {new Coordinate3D(Position.X + XLength, Position.Y + YLength, Position.Z + ZLength)}"
)]
public class Cuboid
{
    public Coordinate3D Position { get; set; }
    public long XLength { get; set; }
    public long YLength { get; set; }
    public long ZLength { get; set; }

    public long this[Direction3D surface]
    {
        get =>
            surface switch
            {
                Direction3D.Top => ZLength >= 0 ? Position.Z + ZLength : Position.Z,
                Direction3D.Bottom => ZLength >= 0 ? Position.Z : Position.Z + ZLength,
                Direction3D.Left => XLength >= 0 ? Position.X : Position.X + XLength,
                Direction3D.Right => XLength >= 0 ? Position.X + XLength : Position.X,
                Direction3D.Front => YLength >= 0 ? Position.Y + YLength : Position.Y,
                Direction3D.Back => YLength >= 0 ? Position.Y : Position.Y + YLength,
                _ => throw new NotImplementedException(),
            };
    }

    public Cuboid(Coordinate3D position, long xLength, long yLength, long zLength) =>
        (Position, XLength, YLength, ZLength) = (position, xLength, yLength, zLength);

    public Cuboid(Coordinate3D a, Coordinate3D b)
    {
        Position = a;
        XLength = b.X - a.X;
        YLength = b.Y - a.Y;
        ZLength = b.Z - a.Z;
    }

    public Cuboid Move(Direction3D direction)
    {
        return Move(direction.ToCoordinate());
    }

    public Cuboid Move(Coordinate3D by)
    {
        this.Position += by;
        return this;
    }

    public bool Contains(Coordinate3D coordinate)
    {
        return (
                XLength >= 0
                    ? coordinate.X >= Position.X && coordinate.X <= Position.X + XLength
                    : coordinate.X <= Position.X && coordinate.X >= Position.X + XLength
            )
            && (
                YLength >= 0
                    ? coordinate.Y >= Position.Y && coordinate.Y <= Position.Y + YLength
                    : coordinate.Y <= Position.Y && coordinate.Y >= Position.Y + YLength
            )
            && (
                ZLength >= 0
                    ? coordinate.Z >= Position.Z && coordinate.Z <= Position.Z + ZLength
                    : coordinate.Z <= Position.Z && coordinate.Z >= Position.Z + ZLength
            );
    }

    public IEnumerable<Coordinate3D> EnumerateSurface(Direction3D surface)
    {
        return surface switch
        {
            Direction3D.Top => EnumerateTop(),
            Direction3D.Bottom => EnumerateBottom(),
            Direction3D.Left => EnumerateLeft(),
            Direction3D.Right => EnumerateRight(),
            Direction3D.Front => EnumerateFront(),
            Direction3D.Back => EnumerateBack(),
            _ => throw new NotImplementedException(),
        };
    }

    public IEnumerable<Coordinate3D> EnumerateTop()
    {
        for (
            long x = Position.X;
            XLength >= 0 ? x <= Position.X + XLength : x >= Position.X + XLength;
            x += XLength == 0 ? 1 : Math.Sign(XLength)
        )
        {
            for (
                long y = Position.Y;
                YLength >= 0 ? y <= Position.Y + YLength : y >= Position.Y + YLength;
                y += YLength == 0 ? 1 : Math.Sign(YLength)
            )
            {
                yield return new Coordinate3D(
                    x,
                    y,
                    ZLength >= 0 ? Position.Z + ZLength : Position.Z
                );
            }
        }
    }

    public IEnumerable<Coordinate3D> EnumerateBottom()
    {
        for (
            long x = Position.X;
            XLength >= 0 ? x <= Position.X + XLength : x >= Position.X + XLength;
            x += XLength == 0 ? 1 : Math.Sign(XLength)
        )
        {
            for (
                long y = Position.Y;
                YLength >= 0 ? y <= Position.Y + YLength : y >= Position.Y + YLength;
                y += YLength == 0 ? 1 : Math.Sign(YLength)
            )
            {
                yield return new Coordinate3D(
                    x,
                    y,
                    ZLength >= 0 ? Position.Z : Position.Z + ZLength
                );
            }
        }
    }

    public IEnumerable<Coordinate3D> EnumerateRight()
    {
        for (
            long y = Position.Y;
            YLength >= 0 ? y <= Position.Y + YLength : y >= Position.Y + YLength;
            y += YLength == 0 ? 1 : Math.Sign(YLength)
        )
        {
            for (
                long z = Position.Z;
                ZLength >= 0 ? z <= Position.Z + ZLength : z >= Position.Z + ZLength;
                z += ZLength == 0 ? 1 : Math.Sign(ZLength)
            )
            {
                yield return new Coordinate3D(
                    XLength >= 0 ? Position.X + XLength : Position.X,
                    y,
                    z
                );
            }
        }
    }

    public IEnumerable<Coordinate3D> EnumerateLeft()
    {
        for (
            long y = Position.Y;
            YLength >= 0 ? y <= Position.Y + YLength : y >= Position.Y + YLength;
            y += YLength == 0 ? 1 : Math.Sign(YLength)
        )
        {
            for (
                long z = Position.Z;
                ZLength >= 0 ? z <= Position.Z + ZLength : z >= Position.Z + ZLength;
                z += ZLength == 0 ? 1 : Math.Sign(ZLength)
            )
            {
                yield return new Coordinate3D(
                    XLength >= 0 ? Position.X : Position.X + XLength,
                    y,
                    z
                );
            }
        }
    }

    public IEnumerable<Coordinate3D> EnumerateFront()
    {
        for (
            long x = Position.X;
            XLength >= 0 ? x <= Position.X + XLength : x >= Position.X + XLength;
            x += XLength == 0 ? 1 : Math.Sign(XLength)
        )
        {
            for (
                long z = Position.Z;
                ZLength >= 0 ? z <= Position.Z + ZLength : z >= Position.Z + ZLength;
                z += ZLength == 0 ? 1 : Math.Sign(ZLength)
            )
            {
                yield return new Coordinate3D(
                    x,
                    YLength >= 0 ? Position.Y + YLength : Position.Y,
                    z
                );
            }
        }
    }

    public IEnumerable<Coordinate3D> EnumerateBack()
    {
        for (
            long x = Position.X;
            XLength >= 0 ? x <= Position.X + XLength : x >= Position.X + XLength;
            x += XLength == 0 ? 1 : Math.Sign(XLength)
        )
        {
            for (
                long z = Position.Z;
                ZLength >= 0 ? z <= Position.Z + ZLength : z >= Position.Z + ZLength;
                z += ZLength == 0 ? 1 : Math.Sign(ZLength)
            )
            {
                yield return new Coordinate3D(
                    x,
                    YLength >= 0 ? Position.Y : Position.Y + YLength,
                    z
                );
            }
        }
    }

    internal bool Intersects(Cuboid other)
    {
        return !(
            this[Direction3D.Left] > other[Direction3D.Right]
            || this[Direction3D.Right] < other[Direction3D.Left]
            || this[Direction3D.Bottom] > other[Direction3D.Top]
            || this[Direction3D.Top] < other[Direction3D.Bottom]
            || this[Direction3D.Back] > other[Direction3D.Front]
            || this[Direction3D.Front] < other[Direction3D.Back]
        );
    }
}
