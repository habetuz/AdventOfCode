using AdventOfCode.PartSubmitter;

public class SimplePartSubmitter : SimplePartSubmitter<object>, IPartSubmitter { }

public class SimplePartSubmitter<Parts>
  : SimplePartSubmitter<Parts, Parts>,
    IPartSubmitter<Parts> { }

public class SimplePartSubmitter<Part1, Part2> : IPartSubmitter<Part1, Part2>
{
  public Part1? FirstPart { get; private set; } = default!;
  public Part2? SecondPart { get; private set; } = default!;

  public bool IsPart1Complete => FirstPart is not null;

  public bool IsPart2Complete => SecondPart is not null;

  public void SubmitPart1(Part1 part)
  {
    FirstPart = part;
  }

  public void SubmitPart2(Part2 part)
  {
    SecondPart = part;
  }
}
