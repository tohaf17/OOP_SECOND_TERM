using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System;
namespace Laba_4;
public class PerformanceViewModel : INotifyPropertyChanged
{
    public IEnumerable<Work> WorkTypes => Enum.GetValues(typeof(Work)).Cast<Work>();

    public Work The_Work { get; set; }

    public string PerformerFullName { get; set; }

    public int Duration { get; set; }

    public string Title { get; set; }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string prop = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    public Performance BuildPerformance()
    {
        string[] parts = PerformerFullName?.Split(' ', 2) ?? new[] { "", "" };
        var performer = new Performer(parts[0], parts.Length > 1 ? parts[1] : "");
        return new Performance(The_Work, performer, Duration, Title);
    }
}