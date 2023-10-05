namespace AdventOfCode
{
    public class InputManager : IInputRetriever, IInputSubmitter
    {
        public InputManager(WebRessourceManager webRessourceManager)
        {
            WebRessourceManager = webRessourceManager;
        }

        public WebRessourceManager WebRessourceManager { get; set; }

        public Solution? RetrieveExampleSolution(CalendarRange.Date date, uint? example)
        {
            if (example is null)
            {
                return null;
            }
            throw new NotImplementedException();
        }

        public string RetrieveInput(CalendarRange.Date date, uint? test = null)
        {
            throw new NotImplementedException();
        }

        public void TouchInput(CalendarRange.Date date, uint? test)
        {
            throw new NotImplementedException();
        }
    }
}