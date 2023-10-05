namespace AdventOfCode
{
    internal interface IInputRetriever
    {
        public string RetrieveInput(Date date, uint? example = null);
        public Solution? RetrieveExampleSolution(Date date, uint? example);
    }
}