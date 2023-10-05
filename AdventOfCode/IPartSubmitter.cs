namespace AdventOfCode
{
    public interface IPartSubmitter<TPuzzle1, TPuzzle2>
    {
        public void SubmitPart1(TPuzzle1 part);
        public void SubmitPart2(TPuzzle2 part);
    }

    public interface IPartSubmitter<TPuzzles> : IPartSubmitter<TPuzzles, TPuzzles>
    {
        public void SubmitFull(TPuzzles full)
        {
            this.SubmitPart1(full);
            this.SubmitPart2(full);
        }
    }

    public interface IPartSubmitter : IPartSubmitter<string>
    {
    }
}