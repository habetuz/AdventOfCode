using AdventOfCode.PartSubmitter;
using AdventOfCode.Solver;
using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D19;

public class Solver : ISolver<(IWorkflow In, Part[] Parts)>
{
  public static long MAX_VALUE = 4000;
  public static long MIN_VALUE = 1;

  public void Parse(string input, IPartSubmitter<(IWorkflow In, Part[] Parts)> partSubmitter)
  {
    var inputParts = input.Split("\n\n");
    var workflowLines = inputParts[0].Split('\n', StringSplitOptions.RemoveEmptyEntries);
    var partLines = inputParts[1].Split('\n', StringSplitOptions.RemoveEmptyEntries);

    // Populate workflows
    var workflows = new Dictionary<string, IWorkflow>(
      workflowLines.Select(
        (line) =>
        {
          var parts = line.Split((char[])['{', '}'], StringSplitOptions.RemoveEmptyEntries);
          var name = parts[0];

          return new KeyValuePair<string, IWorkflow>(name, new Workflow { Name = name });
        }
      )
    )
    {
      { "R", new RejectWorkflow() },
      { "A", new AcceptWorkflow() },
    };

    // Link workflows
    foreach (var workflowLine in workflowLines)
    {
      var stringParts = workflowLine.Split(
        (char[])['{', '}'],
        StringSplitOptions.RemoveEmptyEntries
      );

      var name = stringParts[0];

      var instructionStrings = stringParts[1].Split(',');

      var elseWorkflow = workflows[instructionStrings[^1]];
      var instructions = instructionStrings
        .Take(instructionStrings.Length - 1)
        .Select(
          (instructionString) =>
          {
            Category category = instructionString[0] switch
            {
              'x' => Category.ExtremelyCool,
              'm' => Category.Musical,
              'a' => Category.Aerodynamic,
              's' => Category.Shiny,
              _ => throw new NotImplementedException(),
            };

            Comparison comparison = instructionString[1] switch
            {
              '<' => Comparison.LessThan,
              '>' => Comparison.GreaterThan,
              _ => throw new NotImplementedException(),
            };

            var parts = instructionString[2..].Split(':');

            int value = int.Parse(parts[0]);
            var workflow = workflows[parts[1]];

            return new Instruction
            {
              Category = category,
              Comparison = comparison,
              Value = value,
              Workflow = workflow,
            };
          }
        )
        .ToArray();

      ((Workflow)workflows[name]).Instructions = instructions;
      ((Workflow)workflows[name]).ElseWorkflow = elseWorkflow;
    }

    // Populate parts
    Part[] parts = partLines
      .Select(
        (line) =>
        {
          var parts = line.Split(',');
          return new Part
          {
            X = int.Parse(parts[0][3..]),
            M = int.Parse(parts[1][2..]),
            A = int.Parse(parts[2][2..]),
            S = int.Parse(parts[3][2..^1]),
          };
        }
      )
      .ToArray();

    partSubmitter.Submit((workflows["in"], parts));
  }

  public void Solve((IWorkflow In, Part[] Parts) input, IPartSubmitter partSubmitter)
  {
    long total = 0;
    foreach (var part in input.Parts)
    {
      if (input.In.Process(part))
      {
        total += part.Value;
      }
    }

    partSubmitter.SubmitPart1(total);

    partSubmitter.SubmitPart2(
      input.In.Process(
        new RangeParts()
        {
          X = new BigRange(MIN_VALUE, MAX_VALUE),
          M = new BigRange(MIN_VALUE, MAX_VALUE),
          A = new BigRange(MIN_VALUE, MAX_VALUE),
          S = new BigRange(MIN_VALUE, MAX_VALUE),
        }
      )
    );
  }
}
