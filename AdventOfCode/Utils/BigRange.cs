namespace AdventOfCode.Utils;

/// <summary>
/// Like <see cref="System.Range"/> but with <see cref="long"/> instead of <see cref="int"/>.
/// </summary>
public struct BigRange : IComparable<BigRange>, IEquatable<BigRange>
{
    public long Start { get; }
    public long End { get; }

    public BigRange(long start, long end)
    {
        Start = start;
        End = end;
    }

    /// <summary>
    /// Checks if a value is contained in the range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is contained in the range.</returns>
    public bool InRange(long value)
    {
        return value >= Start && value <= End;
    }

    /// <summary>
    /// Intersects the range with another range.<br/>
    /// </summary>
    /// <param name="other">The other range to intersect with.</param>
    /// <returns>The intersection of the ranges.</returns>
    public BigRange? Intersect(BigRange other)
    {
        var intersection = new BigRange(Math.Max(Start, other.Start), Math.Min(End, other.End));
        if (intersection.Start > intersection.End)
        {
            return null;
        }

        return intersection;
    }

    public static BigRange operator +(BigRange a, BigRange b)
    {
        if (a.Start > b.End || b.Start > a.End)
        {
            throw new InvalidOperationException("Ranges do not overlap");
        }

        return new BigRange(Math.Min(a.Start, b.Start), Math.Max(a.End, b.End));
    }

    public static BigRange operator +(BigRange a, long offset)
    {
        return new BigRange(a.Start + offset, a.End + offset);
    }

    public static BigRange[] operator -(BigRange a, BigRange b)
    {
        var intersection = a.Intersect(b);
        if (!intersection.HasValue)
        {
            return [a];
        }

        if (intersection.Value.Start == a.Start && intersection.Value.End == a.End)
        {
            return [];
        }

        if (intersection.Value.Start == a.Start)
        {
            return [new BigRange(intersection.Value.End + 1, a.End)];
        }

        if (intersection.Value.End == a.End)
        {
            return [new BigRange(a.Start, intersection.Value.Start - 1)];
        }

        return
        [
            new BigRange(a.Start, intersection.Value.Start - 1),
            new BigRange(intersection.Value.End + 1, a.End)
        ];
    }

    public static BigRange operator -(BigRange a, long offset)
    {
        return new BigRange(a.Start - offset, a.End - offset);
    }

    public static bool operator ==(BigRange a, BigRange b)
    {
        return a.Start == b.Start && a.End == b.End;
    }

    public static bool operator !=(BigRange a, BigRange b)
    {
        return !(a == b);
    }

    public static bool operator <(BigRange a, BigRange b)
    {
        return a.Start < b.Start || (a.Start == b.Start && a.End < b.End);
    }

    public static bool operator >(BigRange a, BigRange b)
    {
        return a.Start > b.Start || (a.Start == b.Start && a.End > b.End);
    }

    public static explicit operator BigRange(Range range) {
        long start = range.Start.IsFromEnd? long.MaxValue - range.Start.Value : range.Start.Value;
        long end = range.End.IsFromEnd? long.MaxValue - range.End.Value : range.Start.Value;
        return new BigRange(start, end);
    }

    public override bool Equals(object? obj)
    {
        return obj is BigRange range && range == this;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Start, End);
    }

    public override string ToString()
    {
        return $"{Start}..{End}";
    }

    public int CompareTo(BigRange other)
    {
        if (this < other)
        {
            return -1;
        }

        if (this > other)
        {
            return 1;
        }

        return 0;
    }

    public bool Equals(BigRange other)
    {
        return this == other;
    }
}
