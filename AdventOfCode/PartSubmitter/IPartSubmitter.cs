namespace AdventOfCode.PartSubmitter
{
    public interface IPartSubmitter<in TPart1, in TPart2>
    {
        public void SubmitPart1(TPart1 part);
        public void SubmitPart2(TPart2 part);

        public bool IsPart1Complete { get; }
        public bool IsPart2Complete { get; }
        public bool IsComplete { get => this.IsPart1Complete && this.IsPart2Complete; }
    }

    public interface IPartSubmitter<in TParts> : IPartSubmitter<TParts, TParts>
    {
        public void Submit(TParts parts)
        {
            this.SubmitPart1(parts);
            this.SubmitPart2(parts);
        }
    }

    public interface IPartSubmitter : IPartSubmitter<object> { }
}
