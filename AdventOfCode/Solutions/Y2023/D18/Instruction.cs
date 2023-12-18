using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D18;

public struct Instruction
{
    public Direction Direction { get; init; }
    public byte Length { get; init; }
    public string Color { get; init; }
}
