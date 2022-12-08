
namespace AdventOfCode.Solutions.Y2022.D07
{
    using System.Collections.Generic;

    internal class Directory
    {
        private uint size = 0;

        internal Directory(Directory parentDirectory)
        {
            this.ParentDirectory = parentDirectory;
            this.SubDirectories = new Dictionary<string, Directory>();
            this.Files = new List<File>();
        }

        internal Directory ParentDirectory { get; set; }

        internal Dictionary<string, Directory> SubDirectories { get; set; }

        internal List<File> Files { get; set; }

        internal uint Size
        {
            get
            {
                if (this.size != 0)
                {
                    return this.size;
                }

                for (int i = 0; i < this.Files.Count; i++)
                {
                    this.size += this.Files[i].Size;
                }

                var subDirectories = new Directory[this.SubDirectories.Count];
                this.SubDirectories.Values.CopyTo(subDirectories, 0);

                for (int i = 0; i < subDirectories.Length; i++)
                {
                    this.size += subDirectories[i].Size;
                }

                return this.size;
            }
        }

        internal void GetCombinedSizeOfDirectories(ref uint combinedSize, uint maximumSize)
        {
            if (this.Size <= maximumSize)
            {
                combinedSize += this.Size;
            }

            var subDirectories = new Directory[this.SubDirectories.Count];
            this.SubDirectories.Values.CopyTo(subDirectories, 0);

            for (int i = 0; i < subDirectories.Length; i++)
            {
                subDirectories[i].GetCombinedSizeOfDirectories(ref combinedSize, maximumSize);
            }
        }

        internal void GetClosestToSize(ref uint size, uint goalSize)
        {
            if (this.size < size && this.size > goalSize)
            {
                size = this.Size;
            }

            var subDirectories = new Directory[this.SubDirectories.Count];
            this.SubDirectories.Values.CopyTo(subDirectories, 0);

            for (int i = 0; i < subDirectories.Length; i++)
            {
                subDirectories[i].GetClosestToSize(ref size, goalSize);
            }
        }
    }
}
