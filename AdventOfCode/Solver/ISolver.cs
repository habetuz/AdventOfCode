using AdventOfCode.PartSubmitter;

namespace AdventOfCode.Solver
{
    public interface ISolver<TPuzzle1, TPuzzle2>
    {
        public void Parse(string input, IPartSubmitter<TPuzzle1, TPuzzle2> partSubmitter);
        public void Solve(TPuzzle1 input1, TPuzzle2 input2, IPartSubmitter partSubmitter);
    }

    public interface ISolver<TPuzzles> : ISolver<TPuzzles, TPuzzles>
    {
        void ISolver<TPuzzles, TPuzzles>.Parse(string input, IPartSubmitter<TPuzzles, TPuzzles> partSubmitter)
        {
            IPartSubmitter<TPuzzles> tmpPartSubmitter = new ForwardingPartSubmitter<TPuzzles>(partSubmitter);
            this.Parse(input, tmpPartSubmitter);
        }

        void ISolver<TPuzzles, TPuzzles>.Solve(TPuzzles input1, TPuzzles input2, IPartSubmitter partSubmitter)
        {
            this.Solve(input1, partSubmitter);
        }

        public void Parse(string input, IPartSubmitter<TPuzzles> partSubmitter);

        public void Solve(TPuzzles input, IPartSubmitter partSubmitter);
    }
}