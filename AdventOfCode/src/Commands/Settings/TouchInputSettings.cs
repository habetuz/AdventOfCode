using System.ComponentModel;
using AdventOfCode.Time;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands.Settings;

public class TouchInputSettings : CommandSettings
{
[Description(
  "The date of the input you want to create/open. Leave empty for the current date."
)]
[CommandArgument(0, "[date]")]
public string? StringDate { get; init; } = "";

[Description("The number of the example you want to create/open.")]
[CommandOption("-e|--example")]
public uint? Example { get; init; }

public Date Date { get; private set; }

public override ValidationResult Validate()
{
  Date date;
  var result = DateConverter.SingleDate(StringDate!, out date);
  if (!result.Successful)
  {
    return result;
  }

  var currentTime = DateTime.Now;

  date.Year = date.Year == -1 ? AOCDateTimeUtils.GetCurrentYear(currentTime) : date.Year;
  date.Day = date.Day == -1 ? AOCDateTimeUtils.GetCurrentDay(currentTime) : date.Day;

  if (
    date.Year > AOCDateTimeUtils.GetCurrentYear(currentTime)
    || date.Year < 2015
    || date.Day
      > (
        currentTime.Year == AOCDateTimeUtils.GetCurrentYear(currentTime)
          ? AOCDateTimeUtils.GetCurrentDay(currentTime)
          : 25
      )
    || date.Day < 1
  )
  {
    return ValidationResult.Error("The provided date is out of range.");
  }

  Date = date;
  return ValidationResult.Success();
}
}
