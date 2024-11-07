namespace AdventOfCode.Solutions.Y2021.D13;

public readonly struct Instruction(Axis axis, int index)
{
  public Axis Axis { get; } = axis;
  public int Index { get; } = index;
}

public enum Axis
{
  X,
  Y,
}
