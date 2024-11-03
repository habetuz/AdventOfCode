using System.Diagnostics;

namespace AdventOfCode.Utils;

[DebuggerDisplay("Value = {Value}")]
public class BiNode<T>(T value, BiNode<T>? left = null, BiNode<T>? right = null)
{
  public T Value { get; set; } = value;
  public BiNode<T>? Left { get; set; } = left;
  public BiNode<T>? Right { get; set; } = right;

  public BiNode<T>? GetDirection(Direction direction) =>
    direction switch
    {
      Direction.Left => Left,
      Direction.Right => Right,
      _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
    };
}
