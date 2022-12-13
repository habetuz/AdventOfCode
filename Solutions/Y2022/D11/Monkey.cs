namespace AdventOfCode.Solutions.Y2022.D11
{
    using System.Collections.Generic;

    internal class Monkey
    {
        private readonly char operation;
        private readonly int @operator;
        private readonly int dividend;
        private readonly int caseTrue;
        private readonly int caseFalse;
        private readonly int index;

        internal Monkey(Queue<int> items, char operation, int @operator, int dividend, int caseTrue, int caseFalse, int index)
        {
            this.Items = items;
            this.operation = operation;
            this.@operator = @operator;
            this.dividend = dividend;
            this.caseTrue = caseTrue;
            this.caseFalse = caseFalse;
            this.index = index;
            this.ItemsCompressed = new Queue<int[]>();
        }

        internal Queue<int> Items { get; set; }

        internal Queue<int[]> ItemsCompressed { get; set; }

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

        internal (int[], int) InspectHard()
        {
            var item = this.ItemsCompressed.Dequeue();

            for (int i = 0; i < item.Length; i++)
            {
                var @operator = this.@operator == -1 ? item[i] : this.@operator;

                if (this.operation == '*')
                {
                    item[i] *= @operator;
                }
                else
                {
                    item[i] += @operator;
                }
            }

            if (item[this.index] % this.dividend == 0)
            {
                return (item, this.caseTrue);
            }
            else
            {
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
                this.caseFalse,
                this.index);
        }
    }
}
