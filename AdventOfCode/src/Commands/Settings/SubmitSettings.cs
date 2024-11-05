using System.ComponentModel;
using AdventOfCode.Time;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands.Settings;

public class SubmitSettings : CommandSettings
{
  [Description("The date of the input you want to submit as solution.")]
  [CommandArgument(0, "<date>")]
  public string? StringDate { get; init; } = "";

  [Description("The solution for puzzle one.")]
  [CommandArgument(1, "<solution 1>")]
  public string? Solution1 { get; init; }

  [Description("The solution for puzzle two.")]
  [CommandArgument(1, "[solution 2]")]
  public string? Solution2 { get; init; }

  public Date Date { get; private set; }

  public override ValidationResult Validate()
  {
    var result = DateConverter.SingleDateFull(StringDate!, out Date date);
    if (!result.Successful)
    {
      return result;
    }

    Date = date;

    if (string.IsNullOrEmpty(Solution1) && string.IsNullOrEmpty(Solution2))
    {
      return ValidationResult.Error("No solution provided.");
    }

    return ValidationResult.Success();
  }
}
