using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2015.D06;

public readonly struct Instruction
{
  public InstructionType Type { get; init; }
  public Rectangle Area { get; init; }

  public enum InstructionType
  {
    ENABLE,
    DISABLE,
    TOGGLE,
  }
}
