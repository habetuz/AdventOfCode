namespace AdventOfCode
{
    public class InputManager : IInputRetriever, IInputSubmitter
    {
        public InputManager(WebResourceManager webRessourceManager)
        {
            WebRessourceManager = webRessourceManager;
        }

        public WebResourceManager WebRessourceManager { get; set; }

        public Solution? RetrieveExampleSolution(Date date, uint? example)
        {
            if (example is null)
            {
                return null;
            }
            throw new NotImplementedException();
        }

        public string RetrieveInput(Date date, uint? test = null)
        {
            throw new NotImplementedException();
        }

        public void TouchInput(Date date, uint? test)
        {
            throw new NotImplementedException();
        }
    }
}