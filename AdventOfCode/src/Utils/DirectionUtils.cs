namespace AdventOfCode.Utils;

public static class DirectionUtils
{
  public static IEnumerable<Direction> Iterate(this Direction direction)
  {
    for (
      Direction toCheck = Direction.UpLeft;
      toCheck <= Direction.DownRight;
      toCheck = (Direction)((int)toCheck << 1)
    )
    {
      if (direction.HasFlag(toCheck))
      {
        yield return toCheck;
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

  public static Direction Rotate90Right(this Direction direction)
  {
    if (direction.HasFlag(Direction.All) || direction == 0)
    {
      return direction;
    }

    var rotated = Direction.None;

    if (direction.HasFlag(Direction.UpLeft))
    {
      rotated |= Direction.UpRight;
    }

    if (direction.HasFlag(Direction.Up))
    {
      rotated |= Direction.Right;
    }

    if (direction.HasFlag(Direction.UpRight))
    {
      rotated |= Direction.DownRight;
    }

    if (direction.HasFlag(Direction.Right))
    {
      rotated |= Direction.Down;
    }

    if (direction.HasFlag(Direction.DownRight))
    {
      rotated |= Direction.DownLeft;
    }

    if (direction.HasFlag(Direction.Down))
    {
      rotated |= Direction.Left;
    }

    if (direction.HasFlag(Direction.DownLeft))
    {
      rotated |= Direction.UpLeft;
    }

    if (direction.HasFlag(Direction.Left))
    {
      rotated |= Direction.Up;
    }

    return rotated;
  }

  public static Direction Rotate90Left(this Direction direction)
  {
    if (direction.HasFlag(Direction.All) || direction == 0)
    {
      return direction;
    }

    var rotated = Direction.None;

    if (direction.HasFlag(Direction.UpLeft))
    {
      rotated |= Direction.DownLeft;
    }

    if (direction.HasFlag(Direction.Up))
    {
      rotated |= Direction.Left;
    }

    if (direction.HasFlag(Direction.UpRight))
    {
      rotated |= Direction.UpLeft;
    }

    if (direction.HasFlag(Direction.Right))
    {
      rotated |= Direction.Up;
    }

    if (direction.HasFlag(Direction.DownRight))
    {
      rotated |= Direction.UpRight;
    }

    if (direction.HasFlag(Direction.Down))
    {
      rotated |= Direction.Right;
    }

    if (direction.HasFlag(Direction.DownLeft))
    {
      rotated |= Direction.DownRight;
    }

    if (direction.HasFlag(Direction.Left))
    {
      rotated |= Direction.Down;
    }

    return rotated;
  }

  public static Coordinate ToCoordinate(this Direction direction)
  {
    return direction switch
    {
      Direction.UpLeft => new Coordinate(-1, -1),
      Direction.Up => new Coordinate(0, -1),
      Direction.UpRight => new Coordinate(1, -1),
      Direction.Right => new Coordinate(1, 0),
      Direction.DownRight => new Coordinate(1, 1),
      Direction.Down => new Coordinate(0, 1),
      Direction.DownLeft => new Coordinate(-1, 1),
      Direction.Left => new Coordinate(-1, 0),
      _ => new Coordinate(0, 0),
    };
  }

  public static char ToChar(this Direction direction)
  {
    return direction switch
    {
      Direction.UpLeft => '↖',
      Direction.Up => '↑',
      Direction.UpRight => '↗',
      Direction.Right => '→',
      Direction.DownRight => '↘',
      Direction.Down => '↓',
      Direction.DownLeft => '↙',
      Direction.Left => '←',
      _ => ' ',
    };
  }
}
