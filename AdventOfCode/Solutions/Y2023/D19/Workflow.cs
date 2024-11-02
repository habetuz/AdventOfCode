using System.Diagnostics;

namespace AdventOfCode.Solutions.Y2023.D19;

[DebuggerDisplay("{Name}")]
public class Workflow : IWorkflow
{
  public string Name { get; set; } = null!;

  public Instruction[] Instructions { get; set; } = null!;

  public IWorkflow ElseWorkflow { get; set; } = null!;

  public bool Process(Part part)
  {
    foreach (var instruction in Instructions)
    {
      if (instruction.Process(part, out var workflow))
      {
        return workflow.Process(part);
      }
    }

    return ElseWorkflow.Process(part);
  }

  public long Process(RangeParts? rangeParts)
  {
    long result = 0;

    foreach (var instruction in Instructions)
    {
      if (!rangeParts.HasValue)
      {
        return result;
      }

      if (
        instruction.Process(
          rangeParts.Value,
          out var workflow,
          out var validParts,
          out var remainingParts
        )
      )
      {
        rangeParts = remainingParts;
        result += workflow.Process(validParts);
      }
    }

    if (rangeParts.HasValue)
    {
      result += ElseWorkflow.Process(rangeParts.Value);
    }

    return result;
  }
}
