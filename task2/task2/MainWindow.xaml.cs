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

namespace task2;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public void mouse_enter(object sender, RoutedEventArgs e)
    {
        Random rnd = new Random();
        Button button = (Button)sender;
    
        double newX = rnd.Next(0, (int)(400 - button.ActualWidth));
        double newY = rnd.Next(0, (int)(400 - button.ActualHeight));
    
        Canvas.SetLeft(button, newX);
        Canvas.SetTop(button, newY);
    }

    public void yes_method(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Ви брешете");
    }


}