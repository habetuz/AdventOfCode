namespace AdventOfCode.Solutions.Y2021.D15
{
  using System;

  public class Node(int riskLevel, int x, int y) : IComparable<Node>
  {
    public int RiskLevel { get; } = riskLevel;

    public int X { get; } = x;

    public int Y { get; } = y;

    public int F { get; set; } = 0;

    public bool Discovered { get; set; } = false;

    public int CompareTo(Node? other)
    {
      return other is null ? 1 : F - other.F;
    }
  }
}
