using AdventOfCode;
using AdventOfCode.Commands;
using SharpLog;
using Spectre.Console;
using Spectre.Console.Cli;

CommandApp<RunCommand> app = new();
Logging.Initialize();

app.Configure(config =>
{
    config.AddExample(Array.Empty<string>());
    config.AddExample(["2017"]);
    config.AddExample(["05"]);
    config.AddExample(["2017.05"]);
    config.AddExample(["05...10"]);
    config.AddExample(["17.05..."]);
    config.AddExample(["...14"]);

    config
        .AddCommand<RunCommand>("run")
        .WithDescription("Run the puzzles specified in the range.")
        .WithExample(Array.Empty<string>())
        .WithExample(["run", "2017"])
        .WithExample(["run", "05"])
        .WithExample(["run", "2017.05"])
        .WithExample(["run", "05...10"])
        .WithExample(["run", "17.05..."])
        .WithExample(["run", "...14"]);

    config
        .AddCommand<SaveCookieCommand>("save-cookie")
        .WithAlias("set-cookie")
        .WithDescription("Save the session cookie for future commands.");

    config
        .AddCommand<SetReadmeFileCommand>("set-readme")
        .WithAlias("specify-readme")
        .WithDescription(
            "Sets the path to the README.md file that will be updated when you solve new puzzles."
        );

    config
        .AddCommand<TouchInputCommand>("touch")
        .WithDescription("Create or edit an example input.");

    config
        .AddCommand<SubmitCommand>("submit")
        .WithDescription("Submit a solution to test against in the future.");
#if DEBUG
    config.ValidateExamples();
    config.PropagateExceptions();
#endif
});

try
{
    app.Run(args);
}
catch (Exception error)
{
    AnsiConsole.Write(
        new Rule("[red]Exception occurred during execution![/]")
        {
            Style = "red",
            Border = BoxBorder.Heavy
        }
    );
    Logging.LogError("Exception:", "RUNNER", error);
    var innerException = error.InnerException;
    while (innerException is not null)
    {
        Logging.LogError("Inner exception:", "RUNNER", error);
    }

    Logging.LogFatal("Execution failed!", "RUNNER");
}
