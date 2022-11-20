namespace AdventOfCode.Solutions.Y2021.D08
{
    using AdventOfCode.Common;
    using System;
    using System.Collections.Generic;

    internal class Parser : Parser<Display[]>
    {
        internal override Display[] Parse(string input)
        {
            string[] lines = input.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);

            List<Display> displays = new List<Display>();

            foreach (string line in lines)
            {
                string[] values = line.Split(new string[] { " | " }, StringSplitOptions.RemoveEmptyEntries);
                string[] inputs = values[0].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                string[] outputs = values[1].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                displays.Add(new Display(inputs, outputs));
            }

            return displays.ToArray();
        }
    }
}
