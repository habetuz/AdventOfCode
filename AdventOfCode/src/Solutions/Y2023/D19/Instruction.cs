using System.Diagnostics.CodeAnalysis;
using AdventOfCode.Utils;

namespace AdventOfCode.Solutions.Y2023.D19;

public class Instruction
{
  public IWorkflow Workflow { get; set; }

  public Category Category { get; set; }

  public Comparison Comparison { get; set; }

  public int Value { get; set; }

  public Instruction()
  {
    Workflow = null!;
  }

  public bool Process(Part part, [NotNullWhen(true)] out IWorkflow workflow)
  {
    switch (Comparison)
    {
      case Comparison.GreaterThan:
        if (part[Category] > Value)
        {
          workflow = Workflow;
          return true;
        }
        break;
      case Comparison.LessThan:
        if (part[Category] < Value)
        {
          workflow = Workflow;
          return true;
        }
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }

    workflow = null!;
    return false;
  }

  public bool Process(
    RangeParts parts,
    [NotNullWhen(true)] out IWorkflow workflow,
    [NotNullWhen(true)] out RangeParts validParts,
    out RangeParts? remainingParts
  )
  {
    switch (Comparison)
    {
      case Comparison.GreaterThan:
        if (parts[Category].Contains(Value + 1))
        {
          validParts = parts;
          remainingParts = null;
          validParts[Category] = new BigRange(Value + 1, parts[Category].End);
          if (parts[Category].Start <= Value)
          {
            var temp = parts;
            temp[Category] = new BigRange(parts[Category].Start, Value);
            remainingParts = temp;
          }
          workflow = Workflow;
          return true;
        }
        break;
      case Comparison.LessThan:
        if (parts[Category].Contains(Value - 1))
        {
          validParts = parts;
          remainingParts = null;
          validParts[Category] = new BigRange(parts[Category].Start, Value - 1);
          if (parts[Category].End >= Value)
          {
            var temp = parts;
            temp[Category] = new BigRange(Value, parts[Category].End);
            remainingParts = temp;
          }
          workflow = Workflow;
          return true;
        }
        break;
      default:
        throw new ArgumentOutOfRangeException();
    }

    workflow = null!;
    validParts = default;
    remainingParts = parts;
    return false;
  }
}
