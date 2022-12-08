namespace AdventOfCode.Solutions.Y2022.D07
{
    using System.Collections.Generic;
    using AdventOfCode.Common;

    internal class Parser : Parser<Directory>
    {
        internal override Directory Parse(string input)
        {
            var lines = input.Split('\n');

            var root = new Directory(null);

            var currentDirectory = root;

            for (int i = 2; i < lines.Length; i++)
            {
                var command = lines[i].Split(' ');
                switch (command[0])
                {
                    case "$":
                        if (command[1] == "ls")
                        {
                            continue;
                        }

                        // Change directory
                        switch (command[2])
                        {
                            case "..":
                                currentDirectory = currentDirectory.ParentDirectory;
                                break;
                            case "/":
                                currentDirectory = root;
                                break;
                            default:
                                currentDirectory = currentDirectory.SubDirectories[command[2]];
                                break;
                        }

                        break;
                    case "dir":
                        // Add sub directory
                        currentDirectory.SubDirectories[command[1]] = new Directory(currentDirectory);
                        break;
                    default:
                        // Add file
                        currentDirectory.Files.Add(new File
                        {
                            Size = uint.Parse(command[0]),
                            Name = command[1],
                        });
                        break;
                }
            }

            return root;
        }
    }
}
