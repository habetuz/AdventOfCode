namespace AdventOfCode.Solutions.Y2021.D12;

public class Node
{
  public Node(string name)
  {
    Name = name;
    IsBig = char.IsUpper(Name[0]);
  }

  public Node[] Connections { get; set; } = [];

  public string Name { get; }

  public bool IsBig { get; }
}
