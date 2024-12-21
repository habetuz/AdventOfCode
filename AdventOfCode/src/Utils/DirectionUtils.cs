namespace AdventOfCode.Utils;

public static class DirectionUtils
{
  public static IEnumerable<Direction> Iterate(this Direction direction)
  {
    for (
      Direction toCheck = Direction.UpLeft;
      toCheck <= Direction.Left;
      toCheck = (Direction)((int)toCheck << 1)
    )
    {
      if (direction.HasFlag(toCheck))
      {
        yield return toCheck;
      }
    }
  }

  public static Direction Rotate180(this Direction direction)
  {
    int shifted = (int)direction << 4;
    return (Direction)((shifted & 0xff) | ((shifted & 0xff00) >> 8));
  }

  public static Direction Rotate90Right(this Direction direction)
  {
    int shifted = (int)direction << 2;
    return (Direction)((shifted & 0xff) | ((shifted & 0xff00) >> 8));
  }

  public static Direction Rotate90Left(this Direction direction)
  {
    int shifted = (int)direction << 4 >> 2;
    return (Direction)(((shifted & 0xff0) | ((shifted & 0xf) << 8)) >> 4);
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
