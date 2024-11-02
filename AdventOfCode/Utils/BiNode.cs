using System.Diagnostics;

namespace AdventOfCode.Utils;

[DebuggerDisplay("Value = {Value}")]
public class BiNode<T>
{
    public T Value { get; set; }
    public BiNode<T>? Left { get; set; }
    public BiNode<T>? Right { get; set; }

    public BiNode(T value, BiNode<T>? left = null, BiNode<T>? right = null)
    {
        Value = value;
        Left = left;
        Right = right;
    }

    public BiNode<T>? GetDirection(Direction direction) =>
        direction switch
        {
            Direction.Left => Left,
            Direction.Right => Right,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null),
        };
}
