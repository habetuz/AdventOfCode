using System.Reflection.Metadata.Ecma335;
using AdventOfCode.Time;

namespace AdventOfCode
{
    public class InputManager : IInputRetriever, IInputSubmitter
    {
        private const string INPUT_PATH = "inputs/";

        public InputManager(WebResourceManager webResourceManager)
        {
            this.WebResourceManager = webResourceManager;
        }

        public WebResourceManager WebResourceManager { get; set; }

        public Solution? RetrieveExampleSolution(Date date, uint? example)
        {
            if (example is null)
            {
                return null;
            }
            throw new NotImplementedException();
        }

        public string RetrieveInput(Date date, uint? example = null)
        {
            string filename = INPUT_PATH + $"y{date.Year}.d{date.Day}{(example != null ? ".e" + example : "")}.txt";
            if (File.Exists(filename))
            {
                string file = File.ReadAllText(filename);
                return example == null ? file : this.ParseExample(file).Item2;
            }
            else if (example != null)
            {
                throw new FileNotFoundException("Example file does not exist.", filename);
            }
            else
            {
                string file = this.WebResourceManager.RetrieveResource(date.Year.ToString(), "day", date.Day.ToString(), "input");
                File.Create(filename);
                File.WriteAllText(filename, file);
                return file;
            }
        }

        public void TouchInput(Date date, uint? test)
        {
            throw new NotImplementedException();
        }

        private (string, string) ParseExample(string example)
        {
            throw new NotImplementedException();
        }
    }
}