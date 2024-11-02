using System.ComponentModel;
using Config.Net;
using SharpLog;

namespace AdventOfCode
{
  public static class ApplicationSettings
  {
    static ApplicationSettings()
    {
      Instance.PropertyChanged += (sender, e) =>
        Logging.LogInfo(
          string.Format("Changed application settings property [yellow]{0}[/]", e.PropertyName),
          "RUNNER"
        );
    }

    private const string SETTINGS_FILE = "settings.json";

    public static IApplicationSettings Instance { get; } =
      new ConfigurationBuilder<IApplicationSettings>().UseJsonFile(SETTINGS_FILE).Build();
  }
}
