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

namespace Laba1;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void OnClick_First(object sender, EventArgs e)
    {
        First first = new First();
        first.ShowDialog();
    }
    private void OnClick_Second(object sender, RoutedEventArgs e)
    {
        Second second = new Second();
        second.ShowDialog();
    }
    private void OnClick_Third(object sender, RoutedEventArgs e)
    {
        Third third = new Third();
        third.ShowDialog();
    }
    private void OnClick_Fourth(object sender, RoutedEventArgs e)
    {
        Fourth fourth = new Fourth();
        fourth.ShowDialog();
    }

    private void OnClick_Fifth(object sender, RoutedEventArgs e)
    {
        Fifth fifth = new Fifth();
        fifth.ShowDialog();
    }

}