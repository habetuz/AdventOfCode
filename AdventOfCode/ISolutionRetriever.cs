using AdventOfCode.Time;

namespace AdventOfCode
{
    public interface ISolutionRetriever
    {
        public Solution? Retrieve(Date date);
    }
}
