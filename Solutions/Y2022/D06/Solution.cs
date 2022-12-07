namespace AdventOfCode.Solutions.Y2022.D06
{
    using AdventOfCode.Common;

    internal class Solution : Solution<string>
    {
        internal override (object clipboard, string message) Puzzle1(string input)
        {
            var marker = this.GetStartOfPacketMarkerIndex(input, 4);

            return (marker, $"The first marker appears at index {marker}");
        }

        internal override (object clipboard, string message) Puzzle2(string input)
        {
            var marker = this.GetStartOfPacketMarkerIndex(input, 14);

            return (marker, $"The first marker appears at index {marker}");
        }

        private int GetStartOfPacketMarkerIndex(string stream, int markerLength)
        {
            for (int i = 0; i < stream.Length; i++)
            {
                var window = stream.Substring(i, markerLength);

                var isMarker = true;

                for (int j = 1; j < window.Length; j++)
                {
                    if (window.Substring(j).IndexOf(window[j - 1]) != -1)
                    {
                        isMarker = false;
                        break;
                    }
                }

                if (isMarker)
                {
                    return i + markerLength;
                }
            }

            throw new System.ArgumentException("There is no marker in the provided stream!");
        }
    }
}
