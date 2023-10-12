using AdventOfCode.PartSubmitter;

namespace AdventOfCode.Solver
{
    public interface ISolver<TPuzzle1, TPuzzle2>
    {
        public void Parse(string input, IPartSubmitter<TPuzzle1, TPuzzle2> parsedInput);
        public void Solve(TPuzzle1 input1, TPuzzle2 input2, IPartSubmitter solution);
    }

    public interface ISolver<TPuzzles> : ISolver<TPuzzles, TPuzzles>
    {
        void ISolver<TPuzzles, TPuzzles>.Solve(TPuzzles input1, TPuzzles input2, IPartSubmitter solution)
        {
            this.Solve(input1, solution);
        }

        void ISolver<TPuzzles, TPuzzles>.Parse(string input, IPartSubmitter<TPuzzles, TPuzzles> parsedInput)
        {
            SimplePartSubmitter<TPuzzles> partSubmitter = new();
            this.Parse(input, partSubmitter);
            parsedInput.SubmitPart1(partSubmitter.FirstPart);
            parsedInput.SubmitPart2(partSubmitter.SecondPart);
        }

        public void Solve(TPuzzles input, IPartSubmitter solution);
        
        public void Parse(string input, IPartSubmitter<TPuzzles> parsedInput);
    }
}