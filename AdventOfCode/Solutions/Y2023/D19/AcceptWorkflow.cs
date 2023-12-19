namespace AdventOfCode.Solutions.Y2023.D19;

public class AcceptWorkflow : IWorkflow
{
    public string Name { get; set; }

    public bool Process(Part part)
    {
        return true;
    }

    public long Process(RangeParts? range)
    {
        return range.HasValue ? range.Value.Combinations : 0;
    }

    public AcceptWorkflow()
    {
        Name = "A";
    }
}
