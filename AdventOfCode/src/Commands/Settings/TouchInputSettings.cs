using System.ComponentModel;
using AdventOfCode.Time;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands.Settings;

public class TouchInputSettings : CommandSettings
{
  [Description("The date of the input you want to create/open. Leave empty for the current date.")]
  [CommandArgument(0, "[date]")]
  public string StringDate { get; init; } = "";

  [Description("The number of the example you want to create/open.")]
  [CommandOption("-e|--example")]
  public uint? Example { get; init; }

  public Date Date { get; private set; }

  public override ValidationResult Validate()
  {
    var result = DateConverter.SingleDateFull(StringDate!, out Date date);
    if (!result.Successful)
    {
      return result;
    }

    Date = date;

    return ValidationResult.Success();
  }
}
