using System.Net;
using SharpLog;
using Spectre.Console;

namespace AdventOfCode
{
    public class WebResourceManager
  {
    private const string BASE_URL = "https://adventofcode.com/";
    private HttpClientHandler clientHandler;
    private HttpClient client;

    public WebResourceManager()
    {
      clientHandler = new() { UseCookies = false };

      client = new(clientHandler) { BaseAddress = new Uri(BASE_URL) };

      client.DefaultRequestHeaders.Add("Cookie", $"session={ApplicationSettings.Instance.Cookie}");
      client.DefaultRequestHeaders.UserAgent.ParseAdd(
        "(https://github.com/habetuz/AdventOfCode by mail@marvin-fuchs.de)"
      );
    }

    public string RetrieveResource(params string[] uriParts)
    {
      HttpResponseMessage response = null!;
      AnsiConsole
        .Status()
        .SpinnerStyle("orange1")
        .Spinner(Spinner.Known.BouncingBall)
        .Start(
          $"Downloading {string.Join('/', uriParts)}...",
          (ctx) =>
          {
            response = client.GetAsync(string.Join('/', uriParts)).Result;

            if (!response.IsSuccessStatusCode)
            {
              if (response.StatusCode == HttpStatusCode.BadRequest)
              {
                Logging.LogFatal(
                  "Web request failed because the session cookie is not set or outdated.\nUse [white on black] save-cookie <cookie> [/] to set the session cookie.",
                  "RUNNER"
                );
              }
              else
              {
                Logging.LogFatal(
                  $"Web request failed.[/]\n[red]Status code: [/][white]{response.StatusCode}[/]\n[red]Content: [/][white]{response.Content.ReadAsStringAsync().Result}",
                  "RUNNER"
                );
              }
            }
          }
        );

      return response.Content.ReadAsStringAsync().Result;
    }
  }
}
