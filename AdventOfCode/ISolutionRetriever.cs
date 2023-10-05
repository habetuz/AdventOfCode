namespace AdventOfCode
{
    public interface ISolutionRetriever
    {
        public Solution Retrieve(CalendarRange.Date date);
    }
}