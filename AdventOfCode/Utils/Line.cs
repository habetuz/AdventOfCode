using System.Diagnostics;

namespace AdventOfCode.Utils;

[DebuggerDisplay("({Start}, {End})")]
public struct Line
{
    public Coordinate Start { get; set; }
    public Coordinate End { get; set; }

    public ulong ManhattanLength => Start.ManhattanDistance(End);

    public Line(Coordinate start, Coordinate end) => (Start, End) = (start, end);

    public static bool operator ==(Line a, Line b) => a.Start == b.Start && a.End == b.End;

    public static bool operator !=(Line a, Line b) => !(a == b);

    public override bool Equals(object? obj) => obj is Line line && this == line;

    public override int GetHashCode() => HashCode.Combine(Start, End);

    public static implicit operator Line((Coordinate, Coordinate) tuple) =>
        new Line(tuple.Item1, tuple.Item2);

    public static implicit operator (Coordinate, Coordinate)(Line line) => (line.Start, line.End);
}
