namespace AdventOfCode.PartSubmitter;

public class ForwardingPartSubmitter<ForwardType1, ForwardType2, ReceiveType1, ReceiveType2>
    : IPartSubmitter<ReceiveType1, ReceiveType2>
    where ReceiveType1 : ForwardType1
    where ReceiveType2 : ForwardType2
{
    private IPartSubmitter<ForwardType1, ForwardType2> toForward;

    public IPartSubmitter<ForwardType1, ForwardType2> ToForward
    {
        set => toForward = value;
    }

    public ForwardingPartSubmitter(IPartSubmitter<ForwardType1, ForwardType2> toForward)
    {
        this.toForward = toForward;
    }

    public void SubmitPart1(ReceiveType1 part)
    {
        this.toForward.SubmitPart1(part);
    }

    public void SubmitPart2(ReceiveType2 part)
    {
        this.toForward.SubmitPart2(part);
    }
}
