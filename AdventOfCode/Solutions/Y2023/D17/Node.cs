using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D17;

public class Node: AdventOfCode.Utils.Node<byte>
{
    public Direction EnteringDirection { get; set; }
    public byte StraightCount { get; set; }

    public new int Cost => this.Value;
}