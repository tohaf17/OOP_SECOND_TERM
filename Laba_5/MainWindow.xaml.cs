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

namespace Laba_5
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void ButtonFirstClick(object sender, RoutedEventArgs e)
        {
            Task1 task1 = new Task1();
            task1.Show();
        }
        private void ButtonSecondClick(object sender, RoutedEventArgs e)
        {
            Task2 task2 = new Task2();
            task2.Show();
        }
    }
}