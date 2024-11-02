namespace AdventOfCode.Solutions.Y2023.D12;

public struct Line
{
  public string Data { get; }
  public byte[] Groups { get; }

  public Line(string data, byte[] groups)
  {
    Data = data;
    Groups = groups;
  }
}
