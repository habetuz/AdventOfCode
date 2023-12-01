using AdventOfCode.Time;

namespace AdventOfCode
{
    internal interface IInputRetriever
    {
        public string RetrieveInput(Date date, uint? example = null);
        public Solution? RetrieveExampleSolution(Date date, uint? example);
        public string? RetrieveExampleInput(Date date, uint? example);
    }
}
