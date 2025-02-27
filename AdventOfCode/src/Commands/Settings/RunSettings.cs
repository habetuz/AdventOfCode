using System.ComponentModel;
using AdventOfCode.Time;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands.Settings;

public class RunSettings : CommandSettings
{
  [Description("The date range. Provide no value for all puzzles.")]
  [CommandArgument(1, "[range]")]
  public string Range { get; init; } = null!;

  [Description("Set to enable timed execution.")]
  [CommandOption("-t|--timed")]
  public bool RunTimed { get; init; }

  [Description("The number of the example you want to run.")]
  [CommandOption("-e|--example")]
  public uint? Example { get; init; }

  public CalendarRange RunRange { get; private set; } = null!;

  public override ValidationResult Validate()
  {
    ValidationResult result = DateConverter.DateRange(Range, out CalendarRange runRange);
    if (!result.Successful)
    {
      return result;
    }

    RunRange = runRange;
    return ValidationResult.Success();
  }
}
