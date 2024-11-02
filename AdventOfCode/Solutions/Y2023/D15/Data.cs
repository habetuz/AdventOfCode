namespace AdventOfCode.Solutions.Y2023.D15;

public struct Data
{
    public string Label { get; init; }
    public byte Strength { get; init; }

    public static bool operator ==(Data left, Data right)
    {
        return left.Label == right.Label;
    }

    public override bool Equals(object? obj)
    {
        return obj is Data data && Label == data.Label;
    }

    public static bool operator !=(Data left, Data right)
    {
        return left.Label != right.Label;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Label);
    }
}
