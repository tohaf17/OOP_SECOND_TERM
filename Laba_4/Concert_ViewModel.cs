using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Laba_4;

public class Concert_ViewModel : INotifyPropertyChanged
{
    private Concert concert;

    public Concert_ViewModel()
    {
        concert = new Concert("Default", DateTime.Today);
    }

    public string Organizer
    {
        get => concert.Organizer;
        set
        {
            concert.Organizer = value;
            OnPropertyChanged();
        }
    }

    public DateTime Date
    {
        get => concert.Date;
        set
        {
            concert.Date = value;
            OnPropertyChanged();
        }
    }

    public ObservableCollection<Performance> Performances => concert.Performances;

    public void AddPerformance(Performance perf)
    {
        Performances.Add(perf);
        OnPropertyChanged(nameof(Performances)); // якщо потрібно оновити прив'язку в UI
    }

    public Concert BuildConcert()=>concert;

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string prop = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }
}