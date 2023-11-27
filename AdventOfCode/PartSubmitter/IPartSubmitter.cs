namespace AdventOfCode.PartSubmitter
{
    public interface IPartSubmitter<in TPart1, in TPart2>
    {
        public void SubmitPart1(TPart1 part);
        public void SubmitPart2(TPart2 part);
    }

    public interface IPartSubmitter<in TParts> : IPartSubmitter<TParts, TParts>
    {
        public void SubmitFull(TParts parts)
        {
            this.SubmitPart1(parts);
            this.SubmitPart2(parts);
        }
    }

    public interface IPartSubmitter : IPartSubmitter<object>
    {
    }
}