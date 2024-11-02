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

  public bool IsPart1Complete => toForward.IsPart1Complete;

  public bool IsPart2Complete => toForward.IsPart2Complete;

  public ForwardingPartSubmitter(IPartSubmitter<ForwardType1, ForwardType2> toForward)
  {
    this.toForward = toForward;
  }

  public void SubmitPart1(ReceiveType1 part)
  {
    toForward.SubmitPart1(part);
  }

  public void SubmitPart2(ReceiveType2 part)
  {
    toForward.SubmitPart2(part);
  }
}

public class ForwardingPartSubmitter<ForwardTypes, ReceiveTypes>
  : ForwardingPartSubmitter<ForwardTypes, ForwardTypes, ReceiveTypes, ReceiveTypes>,
    IPartSubmitter<ReceiveTypes>
  where ReceiveTypes : ForwardTypes
{
  public ForwardingPartSubmitter(IPartSubmitter<ForwardTypes, ForwardTypes> toForward)
    : base(toForward) { }
}
