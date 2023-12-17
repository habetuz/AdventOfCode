namespace AdventOfCode.Utils;

public class Node<T> : IComparable<Node<T>>
{
    public T? Value { get; set; }
    public int Cost { get; set; } = 1;
    public Coordinate Position { get; set; }
    public int G { get; set; }
    public int H { get; set; }
    public int F => G + H;

    public int CompareTo(Node<T>? other)
    {
        return this.F.CompareTo(other?.F);
    }
}
