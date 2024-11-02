using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D08;

public struct Input
{
  public Direction[] DirectionSequence { get; }
  public BiNode<string>[] Nodes { get; }

  public Input(Direction[] directionSequence, BiNode<string>[] nodes)
  {
    DirectionSequence = directionSequence;
    Nodes = nodes;
  }
}
