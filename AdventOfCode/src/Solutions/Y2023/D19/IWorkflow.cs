namespace AdventOfCode.Solutions.Y2023.D19;

public interface IWorkflow
{
  string Name { get; set; }

  bool Process(Part part);

  long Process(RangeParts? range);
}
