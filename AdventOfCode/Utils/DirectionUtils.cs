namespace AdventOfCode.Utils;

public static class DirectionUtils
{
    public static IEnumerable<Direction> Iterate(this Direction direction)
    {
        foreach (Direction value in Enum.GetValues(typeof(Direction)).OfType<Direction>())
        {
            if (direction.HasFlag(value) && value != Direction.All && value != Direction.None)
            {
                yield return value;
            }
        }
    }

    public static Direction Invert(this Direction direction)
    {
        if (direction.HasFlag(Direction.All) || direction == 0)
        {
            return direction;
        }

        var inverted = Direction.None;

        if (direction.HasFlag(Direction.UpLeft))
        {
            inverted |= Direction.DownRight;
        }

        if (direction.HasFlag(Direction.Up))
        {
            inverted |= Direction.Down;
        }

        if (direction.HasFlag(Direction.UpRight))
        {
            inverted |= Direction.DownLeft;
        }

        if (direction.HasFlag(Direction.Right))
        {
            inverted |= Direction.Left;
        }

        if (direction.HasFlag(Direction.DownRight))
        {
            inverted |= Direction.UpLeft;
        }

        if (direction.HasFlag(Direction.Down))
        {
            inverted |= Direction.Up;
        }

        if (direction.HasFlag(Direction.DownLeft))
        {
            inverted |= Direction.UpRight;
        }

        if (direction.HasFlag(Direction.Left))
        {
            inverted |= Direction.Right;
        }

        return inverted;
    }
}
