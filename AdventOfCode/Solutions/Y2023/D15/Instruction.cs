using System.Numerics;

namespace AdventOfCode.Solutions.Y2023.D15;

public struct Instruction
{
    public Data Data { get; init; }
    public bool Operation { get; init; }
}
