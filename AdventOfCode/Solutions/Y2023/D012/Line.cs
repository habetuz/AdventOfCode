namespace AdventOfCode.Solutions.Y2023.D12;

public struct Line
{
    public string Data { get; }
    public byte[] Parts { get; }

    public Line(string data, byte[] parts)
    {
        Data = data;
        Parts = parts;
    }
}
