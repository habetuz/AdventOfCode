using System.Diagnostics;

namespace AdventOfCode.Solutions.Y2023.D20;

[DebuggerDisplay("{Name}")]
public class BroadcastModule : IModule
{
  public string Name { get; set; } = "broadcaster";
  public IModule[] Outputs { get; set; } = [];

  public char GetState()
  {
    return '0';
  }

  public (bool pulse, IModule target, IModule sender)[] Process(bool pulse, IModule? caller)
  {
    return Outputs
      .Select(output => (pulse: pulse, target: output, sender: (IModule)this))
      .ToArray();
  }
}
