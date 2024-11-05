using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D05;

public struct AlmanacWithRange
{
  public BigRange[] Seeds { get; set; }
  public LinkedList<Dictionary<BigRange, long>> Maps { get; set; }
}
