using System.Diagnostics;
using AdventOfCode.PartSubmitter;

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

    public void Start()
    {
        this.watch1.Start();
        this.watch2.Start();
    }

    public void SubmitPart1(TPart1 part)
    {
        this.watch1.Stop();
        this.watch2.Stop();
        if (this.part1 is not null)
        {
            throw new AlreadySubmittedException("Part 1 already is submitted!");
        }
        this.part1 = part;
        this.time1 = watch1.Elapsed;
        this.watch2.Start();
    }

    public void SubmitPart2(TPart2 part)
    {
        this.watch1.Stop();
        this.watch2.Stop();
        if (this.part2 is not null)
        {
            throw new AlreadySubmittedException("Part 2 already is submitted!");
        }
        this.part2 = part;
        this.time2 = watch1.Elapsed;
        this.watch1.Start();
    }

    public class PartsNotResolvedException : Exception { }

    public class AlreadySubmittedException : Exception
    {
        public AlreadySubmittedException(string message)
            : base(message) { }
    }
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

    public void Start()
    {
        this.watch1.Start();
        this.watch2.Start();
    }

    public void SubmitPart1(TParts part)
    {
        this.watch1.Stop();
        if (this.part1 is not null)
        {
            throw new Exception("Part 1 already is submitted!");
        }
        this.part1 = part;
        this.time1 = watch1.Elapsed;
        this.watch2.Restart();
    }

    public void SubmitPart2(TParts part)
    {
        this.watch2.Stop();
        if (this.part2 is not null)
        {
            throw new Exception("Part 2 already is submitted!");
        }
        this.part2 = part;
        this.time2 = watch2.Elapsed;
        this.watch1.Restart();
    }
}

public class TimedPartSubmitter : TimedPartSubmitter<object>, IPartSubmitter { }
