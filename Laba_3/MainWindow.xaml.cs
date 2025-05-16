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

namespace Laba_3;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    private List<Button> buttons = new();
    public MainWindow()
    {
        InitializeComponent();
    }

    public async void add_buttons(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(number_from.Text, out int n) ||
            !int.TryParse(number_to.Text, out int to) ||
            !int.TryParse(number_step.Text, out int step) || step <= 0)
        {
            MessageBox.Show("Invalid input");
            return;
        }

        int totalButtons = ((to - n) / step) + 1;
        if (totalButtons > 50000)
        {
            MessageBox.Show("Too many buttons! Max allowed: 50000.");
            return;
        }

        buttonContainer.Items.Clear();
        buttons.Clear();

        await Task.Run(() =>
        {
            for (int i = n; i <= to; i += step)
            {
                int num = i;
                Dispatcher.BeginInvoke(() =>
                {
                    Button btn = new()
                    {
                        Content = num.ToString(),
                        Width = 30,
                        Height = 20,
                        Margin = new Thickness(2)
                    };
                    btn.Click += prime_or_not_number;
                    buttonContainer.Items.Add(btn);
                    buttons.Add(btn);
                });
            }
        });
    }

    public void remove_buttons(object sender, RoutedEventArgs e)
    {
        if (!int.TryParse(number_divide.Text, out int divisor) || divisor == 0)
        {
            MessageBox.Show("Divide by zero");
            return;
        }

        var toRemove = buttons.Where(b => int.TryParse(b.Content.ToString(), out int num) && num % divisor == 0).ToList();

        foreach (var btn in toRemove)
        {
            buttonContainer.Items.Remove(btn);
            buttons.Remove(btn);
        }
    }

    public void prime_or_not_number(object sender, RoutedEventArgs e)
    {
        if (sender is Button button && int.TryParse(button.Content.ToString(), out int number))
        {
            string text = is_prime(number) switch
            {
                -1 => "1 is not composite or prime",
                0 => "The number is composite",
                int divisor => $"The number is composite (divide by {divisor})"
            };
            MessageBox.Show(text);
        }
    }

    public static int is_prime(int number)
    {
        if (number < 2) return -1;
        if (number == 2) return 0;
        if (number % 2 == 0) return 2;

        for (int i = 3; i <= Math.Sqrt(number); i += 2)
        {
            if (number % i == 0)
                return i;
        }

        return 0;
    }



}