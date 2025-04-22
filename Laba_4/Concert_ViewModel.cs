using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace Laba_4;

public class Concert_ViewModel : INotifyPropertyChanged
{
    private Concert concert;
    public RelayCommand OkCommand { get; set; }
    public RelayCommand AddPerformanceCommand { get; set; }
    public Action<Concert> OnSuccess { get; set; }
    public Action<Performance> OnAddPerformance { get; set; }

    
    public Concert_ViewModel()
    {
        concert = new Concert("Default", DateTime.Today);
        OkCommand = new RelayCommand(
            execute: _ =>
            {
                OnSuccess?.Invoke(BuildConcert());
            }
        );
        AddPerformanceCommand = new RelayCommand(
            execute: _ =>
            {
                OnAddPerformance?.Invoke(null);
            }
        );

    }
    public Concert_ViewModel(Concert existingConcert)
    {
        concert = new Concert(existingConcert.Organizer, existingConcert.Date)
        {
            Performances = new ObservableCollection<Performance>(existingConcert.Performances)
        };

        OkCommand = new RelayCommand(
            execute: _ =>
            {
                OnSuccess?.Invoke(BuildConcert());
            }
        );
        AddPerformanceCommand = new RelayCommand(
            execute: _ =>
            {
                OnAddPerformance?.Invoke(null); // просто кажемо View: "відкрий форму!"
            }
        );
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