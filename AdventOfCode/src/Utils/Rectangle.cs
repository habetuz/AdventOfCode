namespace AdventOfCode.Utils;

public struct Rectangle
{
  Coordinate A { get; set; }
  Coordinate B { get; set; }

  public readonly long Size() => (B.X - A.X + 1) * (B.Y - A.Y + 1);

  public Rectangle(Coordinate a, Coordinate b) => (A, B) = (a, b);

  public static bool operator ==(Rectangle a, Rectangle b) => a.A == b.A && a.B == b.B;

  public static bool operator !=(Rectangle a, Rectangle b) => !(a == b);

  public override readonly bool Equals(object? obj) =>
    obj is Rectangle rectangle && this == rectangle;

  public override readonly int GetHashCode() => HashCode.Combine(A, B);

  public readonly IEnumerable<Coordinate> Points()
  {
    for (long y = A.Y; y <= B.Y; y++)
    {
      for (long x = A.X; x <= B.X; x++)
      {
        yield return (x, y);
      }
    }
  }

  public static implicit operator Rectangle((Coordinate, Coordinate) tuple) =>
    new(tuple.Item1, tuple.Item2);

  public static implicit operator (Coordinate, Coordinate)(Rectangle rectangle) =>
    (rectangle.A, rectangle.B);
}
