using System.ComponentModel;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands.Settings;

public class SaveCookieSettings : CommandSettings
{
  [Description("The session cookie. Retrieve it from your browser.")]
  [CommandArgument(0, "<cookie>")]
  public string Cookie { get; init; } = null!;
}
