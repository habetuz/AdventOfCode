using System.Diagnostics;

namespace AdventOfCode.PartSubmitter;

public class TimedPartSubmitter<TPart1, TPart2> : IPartSubmitter<TPart1, TPart2>
{
  private readonly Stopwatch watch1 = new();
  private readonly Stopwatch watch2 = new();

  private TimeSpan? time1;
  private TimeSpan? time2;

  private TPart1? part1;
  private TPart2? part2;

  public (TPart1?, TPart2?) Parts
  {
    get { return (part1, part2); }
  }

  public (TimeSpan?, TimeSpan?) Times
  {
    get { return (time1.HasValue ? time1.Value : null, time2.HasValue ? time2.Value : null); }
  }

  public bool IsPart1Complete => part1 is not null;

  public bool IsPart2Complete => part2 is not null;

  public void Start()
  {
    watch1.Start();
    watch2.Start();
  }

  public void SubmitPart1(TPart1 part)
  {
    watch1.Stop();
    watch2.Stop();
    if (part1 is not null)
    {
      throw new AlreadySubmittedException("Part 1 already is submitted!");
    }
    part1 = part;
    time1 = watch1.Elapsed;
    watch2.Start();
  }

  public void SubmitPart2(TPart2 part)
  {
    watch1.Stop();
    watch2.Stop();
    if (part2 is not null)
    {
      throw new AlreadySubmittedException("Part 2 already is submitted!");
    }
    part2 = part;
    time2 = watch1.Elapsed;
    watch1.Start();
  }

  public class PartsNotResolvedException : Exception { }

  public class AlreadySubmittedException(string message) : Exception(message) { }
}

public class TimedPartSubmitter<TParts> : IPartSubmitter<TParts>
{
  private readonly Stopwatch watch1 = new();
  private readonly Stopwatch watch2 = new();

  private TimeSpan? time1;
  private TimeSpan? time2;

  private TParts? part1;
  private TParts? part2;

  public (TParts?, TParts?) Parts
  {
    get { return (part1, part2); }
  }

  public (TimeSpan?, TimeSpan?) Times
  {
    get { return (time1.HasValue ? time1.Value : null, time2.HasValue ? time2.Value : null); }
  }

  public bool IsPart1Complete => part1 is not null;

  public bool IsPart2Complete => part2 is not null;

  public void Start()
  {
    watch1.Start();
    watch2.Start();
  }

  public void SubmitPart1(TParts part)
  {
    watch1.Stop();
    if (part1 is not null)
    {
      throw new Exception("Part 1 already is submitted!");
    }
    part1 = part;
    time1 = watch1.Elapsed;
    watch2.Restart();
  }

  public void SubmitPart2(TParts part)
  {
    watch2.Stop();
    if (part2 is not null)
    {
      throw new Exception("Part 2 already is submitted!");
    }
    part2 = part;
    time2 = watch2.Elapsed;
    watch1.Restart();
  }
}

public class TimedPartSubmitter : TimedPartSubmitter<object>, IPartSubmitter { }
