using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace Laba_4;

public class Concert:INotifyPropertyChanged
{
    private string organizer;

    public string Organizer
    {
        get=>organizer;
        set
        {
                organizer = value;
                OnPropertyChanged();
            
        }
    }

    private DateTime date;

    public DateTime Date
    {
        get => date;
        set
        {
                date = value;
                OnPropertyChanged();
            
        }
    }
    public ObservableCollection<Performance> Performances { get; set; }=new(); 

    public Concert(string organizer, DateTime date)
    {
        this.organizer = organizer;
        this.date = date;
    }
    public void Change_List_Perfomances()
    {
        
    }

    public override string ToString() => $"🎵 {Organizer} — {Date.ToShortDateString()}";

    public string ToShortString()
    {
        return $"";
    }
    
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

public class Performance:INotifyPropertyChanged
{
    private Work the_work;

    public Work The_Work
    {
        get=>the_work;
        set
        {
            the_work = value;
            OnPropertyChanged();
        }
    }
    private Performer the_performer;
    public Performer The_Performer;

    public string PerformerFullName
    {
        get => $"{The_Performer?.Name} {The_Performer?.Surname}";
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                var parts = value.Split(' ', 2);
                The_Performer.Name = parts[0];
                The_Performer.Surname = parts.Length > 1 ? parts[1] : "";
            }
        }
    }

    
    private int duration;

    public int Duration
    {
        get => duration;
        set
        {
            duration = value;
            OnPropertyChanged();
        }
    } 
    private string title;

    public string Title
    {
        get => title;
        set
        {
            title = value;
            OnPropertyChanged();
        }
    }
    

    public Performance(Work work, Performer performer, int duration, string title)
    {
        the_work = work;
        the_performer = performer;
        this.duration = duration;
        this.title = title;
    }
    
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

public class Performer:INotifyPropertyChanged
{
    private string name;

    public string Name
    {
        get=>name;
        set
        {
            name=value;
            OnPropertyChanged();
        }
    }
    private string surname;

    public string Surname
    {
        get=>surname;
        set
        {
            surname = value;
            OnPropertyChanged();
        }
    }

    public Performer(string name, string surname)
    {
        this.name = name;
        this.surname = surname;
    }
    public event PropertyChangedEventHandler PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

public enum Work
{
    Instrumental,
    Vocal,
    Poetic,
    Prose
}