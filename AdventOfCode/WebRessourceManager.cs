using System.Net;
using SharpLog;

namespace AdventOfCode
{
    public class WebResourceManager
    {
        private const string BASE_URL = "https://adventofcode.com/";
        private HttpClientHandler clientHandler;
        private HttpClient client;

        public WebResourceManager()
        {
            clientHandler = new()
            {
                UseCookies = false,
            };

            client = new(this.clientHandler)
            {
                BaseAddress = new Uri(BASE_URL),
            };

            client.DefaultRequestHeaders.Add("Cookie", $"session={ApplicationSettings.Instance.Cookie}");
            client.DefaultRequestHeaders.UserAgent.ParseAdd("(https://github.com/habetuz/AdventOfCode by mail@marvin-fuchs.de)");
        }

        public string RetrieveResource(params string[] uriParts)
        {
            return client.GetStringAsync(string.Join('/', uriParts)).Result;
        }
    }
}