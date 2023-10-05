using System.ComponentModel;

namespace AdventOfCode
{
    public interface IApplicationSettings : INotifyPropertyChanged
    {
        public string? Cookie { get; set; }
    }
}