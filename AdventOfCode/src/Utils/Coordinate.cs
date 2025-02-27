namespace AdventOfCode.Utils
{
  public struct Coordinate
  {
    public long X { get; set; }
    public long Y { get; set; }

    public Coordinate(long x, long y) => (X, Y) = (x, y);

    public static Coordinate operator +(Coordinate a, Coordinate b) => new(a.X + b.X, a.Y + b.Y);

    public static Coordinate operator -(Coordinate a, Coordinate b) => new(a.X - b.X, a.Y - b.Y);

    public static Coordinate operator -(Coordinate a, long b) => new(a.X - b, a.Y - b);

    public static Coordinate operator *(Coordinate a, long b) => new(a.X * b, a.Y * b);

    public static Coordinate operator *(long a, Coordinate b) => b * a;

    public static Coordinate operator /(Coordinate a, long b) => new(a.X / b, a.Y / b);

    public static Coordinate operator +(Coordinate a, Direction direction)
    {
      switch (direction)
      {
        case Direction.UpLeft:
          a.X--;
          a.Y--;
          break;
        case Direction.Up:
          a.Y--;
          break;
        case Direction.UpRight:
          a.X++;
          a.Y--;
          break;
        case Direction.Right:
          a.X++;
          break;
        case Direction.DownRight:
          a.X++;
          a.Y++;
          break;
        case Direction.Down:
          a.Y++;
          break;
        case Direction.DownLeft:
          a.X--;
          a.Y++;
          break;
        case Direction.Left:
          a.X--;
          break;
      }

      return a;
    }

    public static Coordinate operator -(Coordinate a, Direction direction)
    {
      switch (direction)
      {
        case Direction.UpLeft:
          a.X--;
          a.Y--;
          break;
        case Direction.Up:
          a.Y++;
          break;
        case Direction.UpRight:
          a.X--;
          a.Y++;
          break;
        case Direction.Right:
          a.X--;
          break;
        case Direction.DownRight:
          a.X--;
          a.Y--;
          break;
        case Direction.Down:
          a.Y--;
          break;
        case Direction.DownLeft:
          a.X++;
          a.Y--;
          break;
        case Direction.Left:
          a.X++;
          break;
      }

      return a;
    }

    public static implicit operator Coordinate((int, int) coordinate)
    {
      return new Coordinate(coordinate.Item1, coordinate.Item2);
    }

    public static implicit operator Coordinate((long, long) coordinate) =>
      new(coordinate.Item1, coordinate.Item2);

    public static implicit operator Direction(Coordinate coordinate)
    {
      if (coordinate.X == 0 && coordinate.Y == -1)
      {
        return Direction.Up;
      }

      if (coordinate.X == 1 && coordinate.Y == -1)
      {
        return Direction.UpRight;
      }

      if (coordinate.X == 1 && coordinate.Y == 0)
      {
        return Direction.Right;
      }

      if (coordinate.X == 1 && coordinate.Y == 1)
      {
        return Direction.DownRight;
      }

      if (coordinate.X == 0 && coordinate.Y == 1)
      {
        return Direction.Down;
      }

      if (coordinate.X == -1 && coordinate.Y == 1)
      {
        return Direction.DownLeft;
      }

      if (coordinate.X == -1 && coordinate.Y == 0)
      {
        return Direction.Left;
      }

      if (coordinate.X == -1 && coordinate.Y == -1)
      {
        return Direction.UpLeft;
      }

      return Direction.None;
    }

    public static implicit operator (long, long)(Coordinate coordinate)
    {
      return (coordinate.X, coordinate.Y);
    }

    public static bool operator ==(Coordinate a, Coordinate b) => a.X == b.X && a.Y == b.Y;

    public static bool operator !=(Coordinate a, Coordinate b) => a.X != b.X || a.Y != b.Y;

    public override readonly bool Equals(object? obj) =>
      obj is Coordinate coordinate && this == coordinate;

    public override readonly int GetHashCode() => HashCode.Combine(X, Y);

    public override readonly string ToString() => $"({X}, {Y})";

    public readonly ulong ManhattanDistance(Coordinate coordinate)
    {
      return (ulong)(Math.Abs(X - coordinate.X) + Math.Abs(Y - coordinate.Y));
    }

    public readonly IEnumerable<Coordinate> GetNeighbors(params Direction[] directions)
    {
      foreach (var direction in directions)
      {
        yield return this + direction;
      }
    }

    public readonly void Deconstruct(out long x, out long y)
    {
      x = X;
      y = Y;
    }

    public readonly bool IsInSpace(Coordinate downRight, Coordinate upLeft = default)
    {
      return X >= upLeft.X && Y >= upLeft.Y && X <= downRight.X && Y <= downRight.Y;
    }
  }
}
