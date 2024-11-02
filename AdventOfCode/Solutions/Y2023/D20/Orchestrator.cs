namespace AdventOfCode.Solutions.Y2023.D20;

public class Orchestrator
{
    private readonly IModule[] modules;

    private readonly BroadcastModule broadcastModule;

    private readonly IModule rxModule;

    private bool rxReceivedLow;

    public bool RxReceivedLow
    {
        get => rxReceivedLow;
    }

    public Orchestrator(IModule[] modules)
    {
        this.modules = modules;

        foreach (var module in modules)
        {
            if (module.Name == "broadcaster")
            {
                broadcastModule = (BroadcastModule)module;
            }
            else if (module.Name == "rx")
            {
                rxModule = module;
            }
        }

        if (broadcastModule == null)
        {
            throw new Exception("No broadcast module found");
        }

        if (rxModule == null)
        {
            throw new Exception("No rx module found");
        }
    }

    public (uint high, uint low) Process(bool buttonPulse)
    {
        Queue<(bool pulse, IModule target, IModule sender)> queue = new();
        queue.Enqueue((buttonPulse, broadcastModule, null!));

        (uint high, uint low) = (0, 0);

        while (queue.Count > 0)
        {
            var (pulse, target, sender) = queue.Dequeue();

            if (pulse)
            {
                high++;
            }
            else
            {
                low++;

                if (target == rxModule)
                {
                    rxReceivedLow = true;
                }
            }

            var results = target.Process(pulse, sender);

            foreach (var result in results)
            {
                queue.Enqueue(result);
            }
        }

        return (high, low);
    }

    public string GetState()
    {
        var state = "";
        foreach (var module in modules)
        {
            state += module.GetState();
        }

        return state;
    }
}
