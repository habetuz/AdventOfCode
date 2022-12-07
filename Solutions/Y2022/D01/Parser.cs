namespace AdventOfCode.Solutions.Y2022.D01
{
    using System.Collections.Generic;
    using AdventOfCode.Common;

    internal class Parser : Parser<uint[][]>
    {
        internal override uint[][] Parse(string input)
        {
            var lines = input.Split('\n');

            List<uint[]> inventories = new List<uint[]>();

            List<uint> currentInventory = new List<uint>();

            for (int i = 0; i < lines.Length; i++)
            {
                uint calories;
                if (uint.TryParse(lines[i], out calories))
                {
                    currentInventory.Add(calories);
                }
                else
                {
                    inventories.Add(currentInventory.ToArray());
                    currentInventory.Clear();
                }
            }

            inventories.Add(currentInventory.ToArray());

            return inventories.ToArray();
        }
    }
}
