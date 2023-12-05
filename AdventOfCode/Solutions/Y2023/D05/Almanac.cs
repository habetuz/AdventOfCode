using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D05;

public struct Almanac {
    public long[] Seeds { get; set; }
    public LinkedList<Dictionary<BigRange, long>> Maps { get; set; }
}