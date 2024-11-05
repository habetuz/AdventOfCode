using System.Runtime.CompilerServices;

namespace AdventOfCode;

public static class ProjectDir
{
  public static string Get()
  {
    return Directory.GetParent(Path.GetDirectoryName(InternalGet())!)!.FullName;
  }

  private static string InternalGet([CallerFilePath] String path = "")
  {
    return path;
  }
}
