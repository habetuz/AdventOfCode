using System.Diagnostics;

namespace AdventOfCode.Solutions.Y2023.D20;

[DebuggerDisplay("%{Name} | {GetState()}")]
public class FlipFlopModule : IModule
{
    private bool state;

    public string Name { get; set; } = "%";

    public IModule[] Outputs { get; set; } = [];

    public char GetState()
    {
        return state ? '1' : '0';
    }

    public (bool pulse, IModule target, IModule sender)[] Process(bool pulse, IModule? caller)
    {
        (uint high, uint low) = pulse ? ((uint)1, (uint)0) : (0, 1);

        if (!pulse)
        {
            state = !state;

            return Outputs
                .Select(output => (pulse: state, target: output, sender: (IModule)this))
                .ToArray();
        }

        return [];
    }
}
