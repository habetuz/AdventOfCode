namespace AdventOfCode.Solutions.Y2025.D01;

public struct Instruction
{
  public Action InstrAction { get; init; }
  public int Value { get; init; }

  public enum Action
  {
    Left,
    Right,
  }
}