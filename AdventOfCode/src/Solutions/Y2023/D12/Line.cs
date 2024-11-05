namespace AdventOfCode.Solutions.Y2023.D12;

public struct Line(string data, byte[] groups)
{
  public string Data { get; } = data;
  public byte[] Groups { get; } = groups;
}
