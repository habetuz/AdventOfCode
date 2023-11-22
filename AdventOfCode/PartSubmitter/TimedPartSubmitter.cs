using System.Diagnostics;
using AdventOfCode.PartSubmitter;

public class TimedPartSubmitter<TPart1, TPart2> : IPartSubmitter<TPart1, TPart2>
{
    private readonly Stopwatch watch1 = new();
    private readonly Stopwatch watch2 = new();

    private TimeSpan? time1;
    private TimeSpan? time2;

    private TPart1? part1;
    private TPart2? part2;


    private (TPart1, TPart2) Parts
    {
        get
        {
            if (part1 is not null && part2 is not null)
            {
                return (part1, part2);
            }

            throw new PartsNotResolvedException();
        }
    }

    private (TimeSpan, TimeSpan) Times
    {
        get
        {
            if (time1 is not null && time2 is not null)
            {
                return (time1.Value, time2.Value);
            }

            throw new PartsNotResolvedException();
        }
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
        public AlreadySubmittedException(string message) : base(message)
        {

        }
    }
}