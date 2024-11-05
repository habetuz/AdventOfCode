using AdventOfCode.Commands.Settings;
using CSharpier;
using SharpLog;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands;

public class InitCommand : Command<InitSettings>
{
  public override int Execute(CommandContext context, InitSettings settings)
  {
    string file = settings.Generator(settings.Date);

    var options = new CodeFormatterOptions() { IndentSize = 2 };

    var result = CodeFormatter.Format(file, options);

    if (result.CompilationErrors.Any())
    {
      foreach (var error in result.CompilationErrors)
      {
        Logging.LogError(error.ToString(), "RUNNER");
      }

      throw new Exception("Formatting the generated code failed unexpectedly!");
    }

    Directory.CreateDirectory(Directory.GetParent(settings.SolutionPath)!.FullName);
    File.WriteAllText(settings.SolutionPath, result.Code);
    return 0;
  }
}
