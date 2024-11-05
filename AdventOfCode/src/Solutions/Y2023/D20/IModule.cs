namespace AdventOfCode.Solutions.Y2023.D20;

public interface IModule
{
  public string Name { get; set; }

  public IModule[] Outputs { get; set; }

  public (bool pulse, IModule target, IModule sender)[] Process(bool pulse, IModule? caller);

  public char GetState();
}
