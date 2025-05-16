using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
namespace Observable_Laba_3;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainViewModel ViewModel { get; set; }
    public MainWindow()
    {
        InitializeComponent();
        ViewModel = new MainViewModel();
        DataContext = ViewModel;
    }

    public void add_buttons(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(number_from.Text, out int from) &&
            int.TryParse(number_to.Text, out int to) &&
            int.TryParse(number_step.Text, out int step)
           )
        {
            ViewModel.GenerateButtonsCommand.Execute((from, to, step));
        }
    }

    public void delete_buttons(object sender, RoutedEventArgs e)
    {
        if (int.TryParse(number_delete.Text, out int multiply))
        {
            ViewModel.DeleteButtonsCommand.Execute(multiply);
        }
    }
}

public class Button_Item
{
    public string Text { get; set; }
    public ICommand Command { get; set; }
}

public class MainViewModel
{
    public ObservableCollection<Button_Item> Buttons { get; set; }
    public ICommand GenerateButtonsCommand { get; }
    public ICommand is_prime { get; }
    
    public ICommand DeleteButtonsCommand { get; }

    public MainViewModel()
    {
        Buttons = new ObservableCollection<Button_Item>();
        GenerateButtonsCommand = new RelayCommand(GenerateButtons);
        is_prime = new RelayCommand(Is_Prime);
        DeleteButtonsCommand = new RelayCommand(delete_buttons);
    }

    
    private void delete_buttons(object parameter)
    {
        if (parameter is int number)
        {
            var toDelete = Buttons
                .Where(button => int.Parse(button.Text) % number == 0)
                .ToList(); 

            foreach (var item in toDelete)
            {
                Buttons.Remove(item);
            }
        }
    }

    private void GenerateButtons(object parameter)
    {
        if (parameter is (int from, int to, int step))
        {
            for (int i = from; i <= to; i += step)
            {
                Buttons.Add(new Button_Item()
                {
                    Text = i.ToString(),
                    Command = new RelayCommand(_=>Is_Prime((object)i))
                });
            }
        }
    }
    public void Is_Prime(object parameter)
    {
        if (!int.TryParse(parameter?.ToString(), out int index))
        {
            MessageBox.Show("Invalid number");
            return;
        }

        if (index == 1)
        {
            MessageBox.Show("1 is not prime or composite");
            return;
        }

        if (index == 2)
        {
            MessageBox.Show("The number is prime");
            return;
        }

        for (int k = 2; k <= Math.Sqrt(index); k++)
        {
            if (index % k == 0)
            {
                MessageBox.Show("The number is composite");
                return;
            }
        }
        MessageBox.Show("The number is prime");
    }

}
public class RelayCommand : ICommand
{
    private readonly Action<object> _execute;
    private readonly Func<object, bool> _canExecute;

    public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
    {
        _execute = execute;
        _canExecute = canExecute;
    }

    public bool CanExecute(object parameter) => _canExecute == null || _canExecute(parameter);

    public void Execute(object parameter) => _execute(parameter);
    

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
}
