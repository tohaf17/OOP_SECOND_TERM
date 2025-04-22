using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.ComponentModel;
using System.Windows;
using System;
namespace Laba_4;
public class PerformanceViewModel : INotifyPropertyChanged,IDataErrorInfo
{
    public IEnumerable<Work> WorkTypes => Enum.GetValues(typeof(Work)).Cast<Work>();

    public Work The_Work { get; set; }

    public string PerformerFullName { get; set; }

    public int Duration { get; set; }

    public string Title { get; set; }
    
    public RelayCommand OkCommand { get; set; }

    public Action<Performance> OnSuccess { get; set; }
    public PerformanceViewModel()
    {
        OkCommand = new RelayCommand(
            execute: _ =>
            {
                if (!IsValid())
                {
                    MessageBox.Show("Please fix all input errors before continuing.",
                        "Validation Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                OnSuccess?.Invoke(BuildPerformance());
            }
        );
    }

    public string this[string name_field]
    {
        get
        {
            return name_field switch
            {
                nameof(PerformerFullName) => (string.IsNullOrWhiteSpace(PerformerFullName)
                    ||PerformerFullName.Trim().Split(' ').Length<2)
                    ? "Input full name of the performer"
                    : null,
                nameof(Duration) => Duration <= 0
                    ? "Input valid the performance duration"
                    : null,

                nameof(Title) => string.IsNullOrWhiteSpace(Title)
                    ? "Input title of the performance"
                    : null
            };
        }
    }
    public string Error => null;
    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string prop = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));

    public Performance BuildPerformance()
    {
        string[] parts = PerformerFullName?.Split(' ', 2) ?? new[] { "", "" };
        var performer = new Performer(parts[0], parts.Length > 1 ? parts[1] : "");
        return new Performance(The_Work, performer, Duration, Title);
    }
    
    public bool IsValid()
    {
        return string.IsNullOrEmpty(this[nameof(PerformerFullName)])
               && string.IsNullOrEmpty(this[nameof(Duration)])
               && string.IsNullOrEmpty(this[nameof(Title)]);
    }

}