using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.IO;
using System.Text.Json;
using System.Windows.Data;
using System.Windows;
using System.Linq;

namespace Laba_4;

public class MainViewModel : INotifyPropertyChanged
{
    public ObservableCollection<Concert> Concerts { get; set; } = new();
    const string FilePath = "concerts.json";

    public RelayCommand AddConcertCommand { get; }
    public RelayCommand EditConcertCommand { get; }

    public MainViewModel()
    {
        LoadConcertsFromFile();

        AddConcertCommand = new RelayCommand(_ => AddConcert());
        EditConcertCommand = new RelayCommand(param => EditConcert(param as Concert));
    }

    public void AddConcert()
    {
        var window = new Concert_Form();
        if (window.ShowDialog() == true)
        {
            Concert newConcert = window.Result;
            Concerts.Add(newConcert);
            MessageBox.Show($"Concert created: {newConcert.Organizer}");
        }
    }

    public void EditConcert(Concert selected)
    {
        if (selected == null)
        {
            MessageBox.Show("Please select a concert");
            return;
        }

        var window = new Concert_Form(selected);
        if (window.ShowDialog() == true)
        {
            Concert updated = window.Result;
            selected.Organizer = updated.Organizer;
            selected.Date = updated.Date;
            selected.Performances = updated.Performances;

            CollectionViewSource.GetDefaultView(Concerts).Refresh();
        }
    }

    public void LoadConcertsFromFile()
    {
        if (!File.Exists(FilePath)) return;

        string json = File.ReadAllText(FilePath);
        var dtoList = JsonSerializer.Deserialize<List<ConcertDTO>>(json);
        if (dtoList == null) return;

        Concerts.Clear();
        foreach (var dto in dtoList)
        {
            Concerts.Add(Concert.FromDTO(dto));
        }
    }

    public void SaveConcertsToFile()
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        var dtoList = Concerts.Select(c => c.ToDTO()).ToList();
        string json = JsonSerializer.Serialize(dtoList, options);
        File.WriteAllText(FilePath, json);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string prop = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
}
