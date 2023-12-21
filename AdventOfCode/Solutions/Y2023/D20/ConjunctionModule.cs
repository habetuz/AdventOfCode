using System.Diagnostics;

namespace AdventOfCode.Solutions.Y2023.D20;

[DebuggerDisplay("&{Name} | {GetState()}")]
public class ConjunctionModule : IModule
{
    public string Name { get; set; } = "&";
    public IModule[] Outputs { get; set; } = [];

    private Dictionary<IModule, bool> states = new();

    public char GetState()
    {
        var onCount = '0';
        foreach (var state in states)
        {
            if (state.Value)
            {
                onCount++;
            }
        }

        return onCount;
    }

    public (bool pulse, IModule target, IModule sender)[] Process(bool pulse, IModule? caller)
    {
        if (!pulse)
        {
            states[caller!] = false;
        }

        if (pulse)
        {
            states[caller!] = true;
        }

        if (states.Count > 0 && states.Values.All(x => x))
        {
            return Outputs
                .Select(output => (pulse: false, target: output, sender: (IModule)this))
                .ToArray();
        }
        else
        {
            return Outputs
                .Select(output => (pulse: true, target: output, sender: (IModule)this))
                .ToArray();
        }
    }

    internal void AddInput(IModule module)
    {
        states[module] = false;
    }
}
