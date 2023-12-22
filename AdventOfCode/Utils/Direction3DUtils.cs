namespace AdventOfCode.Utils;

public static class Direction3DUtils
{
    public static Coordinate3D ToCoordinate(this Direction3D direction)
    {
        return direction switch
        {
            Direction3D.Top => new Coordinate3D(0, 0, 1),
            Direction3D.Bottom => new Coordinate3D(0, 0, -1),
            Direction3D.Left => new Coordinate3D(-1, 0, 0),
            Direction3D.Right => new Coordinate3D(1, 0, 0),
            Direction3D.Front => new Coordinate3D(0, 1, 0),
            Direction3D.Back => new Coordinate3D(0, -1, 0),
            _ => throw new NotImplementedException(),
        };
    }

    public static Direction3D Invert(this Direction3D direction)
    {
        return direction switch
        {
            Direction3D.Top => Direction3D.Bottom,
            Direction3D.Bottom => Direction3D.Top,
            Direction3D.Left => Direction3D.Right,
            Direction3D.Right => Direction3D.Left,
            Direction3D.Front => Direction3D.Back,
            Direction3D.Back => Direction3D.Front,
            _ => throw new NotImplementedException(),
        };
    }
}
