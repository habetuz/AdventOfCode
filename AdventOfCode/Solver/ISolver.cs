using AdventOfCode.PartSubmitter;

namespace AdventOfCode.Solver
{
    public interface ISolver<in TPuzzle1, in TPuzzle2>
    {
        public void Parse<Part1, Part2>(string input, IPartSubmitter<Part1, Part2> parsedInput) where Part1 : TPuzzle1 where Part2 : TPuzzle2;
        public void Solve(TPuzzle1 input1, TPuzzle2 input2, IPartSubmitter solution);
    }

    public interface ISolver<in TPuzzles> : ISolver<TPuzzles, TPuzzles>
    {
        void ISolver<TPuzzles, TPuzzles>.Solve(TPuzzles input1, TPuzzles input2, IPartSubmitter solution)
        {
            this.Solve(input1, solution);
        }

        void ISolver<TPuzzles, TPuzzles>.Parse<Part1, Part2>(string input, IPartSubmitter<Part1, Part2> parsedInput)
        {
            SimplePartSubmitter<TPuzzles> partSubmitter = new();
            this.Parse(input, partSubmitter);
            parsedInput.SubmitPart1((Part1)partSubmitter.FirstPart!);
            parsedInput.SubmitPart2((Part2)partSubmitter.SecondPart!);
        }

        public void Solve(TPuzzles input, IPartSubmitter solution);

        public void Parse(string input, IPartSubmitter<TPuzzles> parsedInput);
    }
}