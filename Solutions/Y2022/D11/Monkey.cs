namespace AdventOfCode.Solutions.Y2022.D11
{
    using System.Collections.Generic;

    internal class Monkey
    {
        private char operation;
        private int @operator;
        private int dividend;
        private int caseTrue;
        private int caseFalse;

        internal Monkey(Queue<int> items, char operation, int @operator, int dividend, int caseTrue, int caseFalse)
        {
            this.Items = items;
            this.operation = operation;
            this.@operator = @operator;
            this.dividend = dividend;
            this.caseTrue = caseTrue;
            this.caseFalse = caseFalse;
        }

        internal Queue<int> Items { get; set; }

        internal int Dividend { get => this.dividend; }

        internal (int, int) Inspect()
        {
            var item = this.Items.Dequeue();

            var @operator = this.@operator == -1 ? item : this.@operator;

            if (this.operation == '*')
            {
                item *= @operator;
            }
            else
            {
                item += @operator;
            }

            item /= 3;

            if (item % this.dividend == 0)
            {
                return (item, this.caseTrue);
            }
            else
            {
                return (item, this.caseFalse);
            }
        }

        internal (int, int) InspectHard(int[] dividends)
        {
            var item = this.Items.Dequeue();

            var @operator = this.@operator == -1 ? item : this.@operator;

            if (this.operation == '*')
            {
                item *= @operator;
            }
            else
            {
                item += @operator;
            }

            // My understanding is that the number has to be the lowest that has the same modulo for all dividends.
            var values = new int[dividends.Length];
            var multipliers = new int[dividends.Length];
            for (int i = 0; i < values.Length; i++)
            {
                multipliers[i] = 1;
                values[i] = dividends[i] - (item % dividends[i]);
            }

            bool correct;

            do
            {
                correct = true;

                for (int i = 1; i < dividends.Length; i++)
                {
                    while (values[i] < values[0])
                    {
                        multipliers[i]++;
                        values[i] = (multipliers[i] * dividends[i]) - (item % dividends[i]);
                    }

                    if (values[i] != values[0])
                    {
                        correct = false;
                    }
                }

                multipliers[0]++;
                values[0] = (multipliers[0] * dividends[0]) - (item % dividends[0]);
            }
            while (!correct);

            item = values[1];

            if (item % this.dividend == 0)
            {
                return (item, this.caseTrue);
            }
            else
            {
                item = this.dividend - (item % this.dividend);
                return (item, this.caseFalse);
            }
        }

        internal Monkey Clone()
        {
            return new Monkey(
                new Queue<int>(this.Items),
                this.operation,
                this.@operator,
                this.dividend,
                this.caseTrue,
                this.caseFalse);
        }
    }
}
