using AdventOfCode;
using Spectre.Console.Cli;
using SharpLog;

CommandApp<AdventRunnerCommand> app = new();
Logging.Initialize();

app.Configure(
    config =>
    {
        config.PropagateExceptions();
        config.AddExample(Array.Empty<string>());
        config.AddExample(new string[] { "[cookie]", "--years", "2021", "-d", "25" });
        config.AddExample(new string[] { "[cookie]", "-y", "..2022", "--days", "05..19" });
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