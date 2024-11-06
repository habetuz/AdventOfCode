namespace AdventOfCode.Solutions.Y2023.D12;

public readonly struct DataLine(string data, byte[] groups)
{
  public string Data { get; } = data;
  public byte[] Groups { get; } = groups;
}
