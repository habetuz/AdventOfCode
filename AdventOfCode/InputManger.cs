using System.Reflection.Metadata.Ecma335;
using AdventOfCode.Time;
using SharpLog;

namespace AdventOfCode
{
    public class InputManager : IInputRetriever, IInputSubmitter
    {
        private const string INPUT_PATH = "inputs/";

        public InputManager(WebResourceManager webResourceManager)
        {
            this.WebResourceManager = webResourceManager;
            if (!Directory.Exists(INPUT_PATH))
            {
                Directory.CreateDirectory(INPUT_PATH);
            }
        }

        public WebResourceManager WebResourceManager { get; set; }

        public Solution? RetrieveExampleSolution(Date date, uint? example)
        {
            if (example is null)
            {
                return null;
            }
            return ParseExample(RetrieveInput(date, example)).Item1;
        }

        public string RetrieveInput(Date date, uint? example = null)
        {
            string filename = INPUT_PATH + $"y{date.Year}.d{date.Day}{(example != null ? ".e" + example : "")}.txt";
            if (File.Exists(filename))
            {
                string file = File.ReadAllText(filename);
                return file;
            }
            else if (example != null)
            {
                throw new FileNotFoundException("Example file does not exist.", filename);
            }
            else
            {
                string file = this.WebResourceManager.RetrieveResource(date.Year.ToString(), "day", date.Day.ToString(), "input");
                File.WriteAllText(filename, file);
                return file;
            }
        }

        public void TouchInput(Date date, uint? example)
        {
            string filename = INPUT_PATH + $"y{date.Year}.d{date.Day}{(example != null ? ".e" + example : "")}.txt";
            if (!File.Exists(filename))
            {
                if (example != null)
                {
                    File.WriteAllText(filename, "<solution1> | <solution2>\n<input>");
                }
                else
                {
                    string file = this.WebResourceManager.RetrieveResource(date.Year.ToString(), "day", date.Day.ToString(), "input");
                    File.WriteAllText(filename, file);
                }
            }


            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(Path.GetFullPath(filename))
            {
                WorkingDirectory = "/",
                UseShellExecute = true,
            });
        }

        private (Solution, string) ParseExample(string example)
        {
            var lines = example.Split('\n');
            var solution = lines[0].Split('|');
            return (
                new Solution()
                {
                    Solution1 = solution[0].Trim(),
                    Solution2 = solution[1].Trim(),
                },
                string.Join('\n', lines[1..])
            );
        }
    }
}