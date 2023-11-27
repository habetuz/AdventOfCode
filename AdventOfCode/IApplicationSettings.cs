using System.ComponentModel;

namespace AdventOfCode
{
    public interface IApplicationSettings : INotifyPropertyChanged
    {
        public string? Cookie { get; set; }
        public string? ReadmePath { get; set; }
    }
}