namespace AdventOfCode
{
    public interface ISolver<TPuzzle1, TPuzzle2>
    {
        public void Parse(string input, IPartSubmitter<TPuzzle1, TPuzzle2> parsedInput);
        public void Solve(TPuzzle1 input1, TPuzzle2 input2, IPartSubmitter solution);
    }

    public interface ISolver<TPuzzles> : ISolver<TPuzzles, TPuzzles>
    {
        new public void Solve(TPuzzles input1, TPuzzles input2, IPartSubmitter solution)
        {
            this.Solve(input1, solution);
        }

        public void Solve(TPuzzles input, IPartSubmitter solution);
    }
}