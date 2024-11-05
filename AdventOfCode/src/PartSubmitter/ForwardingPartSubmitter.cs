namespace AdventOfCode.PartSubmitter;

public class ForwardingPartSubmitter<ForwardType1, ForwardType2, ReceiveType1, ReceiveType2>(
  IPartSubmitter<ForwardType1, ForwardType2> toForward
) : IPartSubmitter<ReceiveType1, ReceiveType2>
  where ReceiveType1 : ForwardType1
  where ReceiveType2 : ForwardType2
{
  private IPartSubmitter<ForwardType1, ForwardType2> toForward = toForward;

  public IPartSubmitter<ForwardType1, ForwardType2> ToForward
  {
    set => toForward = value;
  }

  public bool IsPart1Complete => toForward.IsPart1Complete;

  public bool IsPart2Complete => toForward.IsPart2Complete;

  public void SubmitPart1(ReceiveType1 part)
  {
    toForward.SubmitPart1(part);
  }

  public void SubmitPart2(ReceiveType2 part)
  {
    toForward.SubmitPart2(part);
  }
}

public class ForwardingPartSubmitter<ForwardTypes, ReceiveTypes>(
  IPartSubmitter<ForwardTypes, ForwardTypes> toForward
)
  : ForwardingPartSubmitter<ForwardTypes, ForwardTypes, ReceiveTypes, ReceiveTypes>(toForward),
    IPartSubmitter<ReceiveTypes>
  where ReceiveTypes : ForwardTypes { }
