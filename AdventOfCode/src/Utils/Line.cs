using System.Diagnostics;

namespace AdventOfCode.Utils;

[DebuggerDisplay("({Start}, {End})")]
public struct Line
{
  public Coordinate Start { get; set; }
  public Coordinate End { get; set; }

  public readonly bool IsHorizontal
  {
    get => Start.Y == End.Y;
  }
  public readonly bool IsVertical
  {
    get => Start.X == End.X;
  }

  public readonly bool IsDiagonal
  {
    get => !IsHorizontal && !IsVertical;
  }

  public readonly ulong ManhattanLength => Start.ManhattanDistance(End);

  public Line(Coordinate start, Coordinate end) => (Start, End) = (start, end);

  public static bool operator ==(Line a, Line b) => a.Start == b.Start && a.End == b.End;

  public static bool operator !=(Line a, Line b) => !(a == b);

  public override readonly bool Equals(object? obj) => obj is Line line && this == line;

  public override readonly int GetHashCode() => HashCode.Combine(Start, End);

  public static implicit operator Line((Coordinate, Coordinate) tuple) =>
    new(tuple.Item1, tuple.Item2);

  public static implicit operator (Coordinate, Coordinate)(Line line) => (line.Start, line.End);

  public readonly IEnumerable<Coordinate> Points()
  {
    if (IsHorizontal)
    {
      for (long x = Math.Min(Start.X, End.X); x <= Math.Max(Start.X, End.X); x++)
      {
        yield return (x, Start.Y);
      }
    }
    else if (IsVertical)
    {
      for (long y = Math.Min(Start.Y, End.Y); y <= Math.Max(Start.Y, End.Y); y++)
      {
        yield return (Start.X, y);
      }
    }
    else if (Math.Abs(End.X - Start.X) == Math.Abs(End.Y - Start.Y)) // 45° angle
    {
      long signX = Math.Sign(End.X - Start.X);
      long signY = Math.Sign(End.Y - Start.Y);

      for (long offset = 0; offset <= Math.Abs(End.X - Start.X); offset++)
      {
        yield return (Start.X + (offset * signX), Start.Y + (offset * signY));
      }
    }
    else
    {
      throw new Exception("Line is neither horizontal, vertical or at a 45° angle!");
    }
  }
}
