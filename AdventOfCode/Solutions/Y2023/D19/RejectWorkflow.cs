namespace AdventOfCode.Solutions.Y2023.D19;

public class RejectWorkflow : IWorkflow
{
    public string Name { get; set; }

    public bool Process(Part part)
    {
        return false;
    }

    public long Process(RangeParts? range)
    {
        return 0;
    }

    public RejectWorkflow()
    {
        Name = "R";
    }
}
