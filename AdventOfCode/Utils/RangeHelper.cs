namespace AdventOfCode.Utils;

public struct RangeHelper {
    public static bool InRange(Index value, Range range) {
        return value.Value >= range.Start.Value && value.Value <= range.End.Value;
    }
}