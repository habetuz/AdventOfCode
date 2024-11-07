using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;

namespace AdventOfCode.Solutions.Y2023.D20;

public class Solver : ISolver<Orchestrator>
{
  public void Parse(string input, IPartSubmitter<Orchestrator> partSubmitter)
  {
    var lines = input.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

    // Initialize modules
    Dictionary<string, IModule> modules =
      new(
        lines.Select(
          (line) =>
          {
            var name = line.Split(" -> ")[0];
            switch (name[0])
            {
              case '%':
                return new KeyValuePair<string, IModule>(
                  name[1..],
                  new FlipFlopModule() { Name = name[1..] }
                );
              case '&':
                return new KeyValuePair<string, IModule>(
                  name[1..],
                  new ConjunctionModule() { Name = name[1..] }
                );
              case 'b':
                return new KeyValuePair<string, IModule>(
                  name,
                  new BroadcastModule() { Name = name }
                );
              default:
                throw new Exception("Unknown module type");
            }
          }
        )
      );

    // Link modules
    foreach (var line in lines)
    {
      var parts = line.Split(" -> ");
      var name = parts[0].TrimStart(['%', '&']);
      var outputs = parts[1].Split(", ");

      var module = modules[name];
      module.Outputs = outputs
        .Select(
          (output) =>
          {
            if (!modules.TryGetValue(output, out IModule? value))
            {
              value = new BroadcastModule() { Name = output };
              modules[output] = value;
            }

            if (value is ConjunctionModule conjunctionModule)
            {
              conjunctionModule.AddInput(module);
            }

            return value;
          }
        )
        .ToArray();
    }

    partSubmitter.Submit(new Orchestrator(modules.Values.ToArray()));
  }

  public void Solve(Orchestrator input, IPartSubmitter partSubmitter)
  {
    (uint high, uint low) = (0, 0);

    for (int buttonPresses = 1; !partSubmitter.IsComplete; buttonPresses++)
    {
      var result = input.Process(false);
      high += result.high;
      low += result.low;

      if (buttonPresses == 1000)
      {
        partSubmitter.SubmitPart1(high * low);
        break;
      }

      if (input.RxReceivedLow)
      {
        partSubmitter.SubmitPart2(buttonPresses);
      }
    }
  }
}
