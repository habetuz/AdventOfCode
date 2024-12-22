namespace AdventOfCode.Solutions.Y2024.D07;

public readonly struct Equation(ulong result, ulong[] values)
{
    public ulong Result { get; init; } = result;

    public ulong[] Values { get; init; } = values;
}
