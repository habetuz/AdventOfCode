namespace AdventOfCode
{
    internal interface IInputRetriever
    {
        public string RetrieveInput(CalendarRange.Date date, uint? example = null);
        public Solution? RetrieveExampleSolution(CalendarRange.Date date, uint? example);
    }
}