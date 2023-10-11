using AdventOfCode;
using AdventOfCode.Commands;
using Spectre.Console.Cli;
using SharpLog;

CommandApp<RunCommand> app = new();
Logging.Initialize();

app.Configure(
    config =>
    {
        config.PropagateExceptions();
        config.AddExample(Array.Empty<string>());
        config.AddExample(new string[] { "2017" });
        config.AddExample(new string[] { "05" });
        config.AddExample(new string[] { "2017.05" });
        config.AddExample(new string[] { "05...10" });
        config.AddExample(new string[] { "17.05..." });
        config.AddExample(new string[] { "...14" });

        config.AddCommand<RunCommand>("run")
            .WithDescription("Run the puzzles specified in the range.")
            .WithExample(Array.Empty<string>())
            .WithExample(new string[] { "run", "2017" })
            .WithExample(new string[] { "run", "05" })
            .WithExample(new string[] { "run", "2017.05" })
            .WithExample(new string[] { "run", "05...10" })
            .WithExample(new string[] { "run", "17.05..." })
            .WithExample(new string[] { "run", "...14" });

        config.AddCommand<SaveCookieCommand>("save-cookie")
            .WithAlias("set-cookie")
            .WithDescription("Save the session cookie for future commands.");

        config.AddCommand<TouchInputCommand>("touch")
            .WithDescription("Create or edit an example input.");
#if DEBUG
        config.ValidateExamples();
#endif
    });

try
{
    app.Run(args);
}
catch (Exception error)
{
    Logging.LogFatal("Execution failed!", "RUNNER", error);
}