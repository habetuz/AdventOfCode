namespace AdventOfCode.Utils;

public class GridNode<T>(
  T value,
  GridNode<T>? up = null,
  GridNode<T>? right = null,
  GridNode<T>? down = null,
  GridNode<T>? left = null
)
{
  public T Value { get; set; } = value;
  public GridNode<T>? Up { get; set; } = up;
  public GridNode<T>? Right { get; set; } = right;
  public GridNode<T>? Down { get; set; } = down;
  public GridNode<T>? Left { get; set; } = left;

  public GridNode<T>? this[Direction direction]
  {
    get =>
      direction switch
      {
        Direction.Up => Up,
        Direction.Right => Right,
        Direction.Down => Down,
        Direction.Left => Left,
        _ => throw new ArgumentException(
          $"GridNode does not have neighbors in direction {direction}"
        ),
      };
    set
    {
      switch (direction)
      {
        case Direction.Up:
          Up = value;
          break;
        case Direction.Right:
          Right = value;
          break;
        case Direction.Down:
          Down = value;
          break;
        case Direction.Left:
          Left = value;
          break;
        default:
          throw new ArgumentException($"GridNode does not have neighbors in direction {direction}");
      }
    }
  }
}
