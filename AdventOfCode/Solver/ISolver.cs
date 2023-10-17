using AdventOfCode.PartSubmitter;

namespace AdventOfCode.Solver
{
    public interface ISolver<in TPuzzle1, in TPuzzle2>
    {
        public delegate void SubmitParsedPart1(TPuzzle1 parsedInput);
        public delegate void SubmitParsedPart2(TPuzzle2 parsedInput);
        public delegate void SubmitSolutionPart1(string solution);
        public delegate void SubmitSolutionPart2(string solution);

        public event EventHandler<> SubmitParsedPart1;

        public void Parse(string input);
        public void Solve(TPuzzle1 input1, TPuzzle2 input2);
    }

    public interface ISolver<TPuzzles> : ISolver<TPuzzles, TPuzzles>
    {
        public void SubmitParsedFull(TPuzzles parsedInput)
        {
            SubmitParsedPart1;
        }

        void ISolver<TPuzzles, TPuzzles>.Solve(TPuzzles input1, TPuzzles input2)
        {
            this.Solve(input1);
        }

        void ISolver<TPuzzles, TPuzzles>.Parse(string input)
        {
            this.Parse(input);
        }

        public void Solve(TPuzzles input);

        public void Parse<Parts>(string input) where Parts : TPuzzles;
    }
}