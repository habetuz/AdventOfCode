using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D08;

public struct Input(Direction[] directionSequence, BiNode<string>[] nodes)
{
  public Direction[] DirectionSequence { get; } = directionSequence;
  public BiNode<string>[] Nodes { get; } = nodes;
}
