using AdventOfCode.Commands.Settings;
using CSharpier;
using SharpLog;
using Spectre.Console.Cli;

namespace AdventOfCode.Commands;

public class InitCommand : Command<InitSettings>
{
  public override int Execute(CommandContext context, InitSettings settings)
  {
    string file =
      @$"
      using AdventOfCode.PartSubmitter;
      using AdventOfCode.Solver;

      namespace AdventOfCode.Solutions.Y{settings.Date.Year}.D{settings.Date.Day:D2};
      
      public class Solver : ISolver<string> 
      {{
        public void Parse(string input, IPartSubmitter<string> partSubmitter)
        {{
          throw new NotImplementedException();
        }}

        public void Solve(string input, IPartSubmitter partSubmitter)
        {{
          throw new NotImplementedException();
        }}
      }}
    ";

    var options = new CodeFormatterOptions() { IndentSize = 2 };

    var result = CodeFormatter.Format(file, options);

    if (result.CompilationErrors.Any())
    {
      foreach (var error in result.CompilationErrors)
      {
        Logging.LogError(error.GetMessage(), "RUNNER");
      }

      throw new Exception("Formatting the generated code failed unexpectedly!");
    }

    Directory.CreateDirectory(Directory.GetParent(settings.SolutionPath)!.FullName);
    File.WriteAllText(settings.SolutionPath, result.Code);
    return 0;
  }
}
