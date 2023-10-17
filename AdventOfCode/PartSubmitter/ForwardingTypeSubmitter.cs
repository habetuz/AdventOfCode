using AdventOfCode.PartSubmitter;

public class ForwardingTypeSubmitter<Parts> : IPartSubmitter<Parts>
{
    private readonly IPartSubmitter<Parts, Parts> toForward;

    public ForwardingTypeSubmitter(IPartSubmitter<Parts, Parts> toForward)
    {
        this.toForward = toForward;
    }

    public void SubmitPart1(Parts part)
    {
        this.toForward.SubmitPart1(part);
    }

    public void SubmitPart2(Parts part)
    {
        this.toForward.SubmitPart2(part);
    }
}