using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Laba1;

public partial class Fifth : Window
{
    public Fifth()
    {
        InitializeComponent();
    }

    private void trans_click(object sender, RoutedEventArgs e)
    {
        Opacity = 1.2 - Opacity;
    }

    private void back_click(object sender, RoutedEventArgs e)
    {
        Button but=sender as Button;
        if ( but.Background== Brushes.LightGray)
            but.Background = Brushes.Yellow;
        else
            but.Background = Brushes.LightGray;
    }

    private void hello_click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show("Goodbye Julia!");
    }

    private void super_click(object sender, RoutedEventArgs e)
    {
        
        MessageBox.Show("I am super! :)");
        if (first.IsChecked == true)
        {
            first_checked(first, new RoutedEventArgs());
        }
        else
        {
            first_unchecked(first, new RoutedEventArgs());
        }

        if (second.IsChecked == true)
        {
            second_checked(second, new RoutedEventArgs());
        }
        else
        {
            second_unchecked(second, new RoutedEventArgs());
        }

        if (third.IsChecked == true)
        {
            third_checked(third, new RoutedEventArgs());
        }
        else
        {
            third_unchecked(third, new RoutedEventArgs());
        }
        
    }
    private void first_checked(object sender, RoutedEventArgs e)
    {
        super.Click += trans_click;
    }

    private void first_unchecked(object sender, RoutedEventArgs e)
    {
        super.Click -= trans_click;
    }
    private void second_checked(object sender, RoutedEventArgs e)
    {
        super.Click += back_click;
    }
    private void second_unchecked(object sender, RoutedEventArgs e)
    {
        super.Click -= back_click;
    }
    private void third_checked(object sender, RoutedEventArgs e)
    {
        super.Click += hello_click;
    }
    private void third_unchecked(object sender, RoutedEventArgs e)
    {
        super.Click -= hello_click;
    }
    
}
