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
        Button but = sender as Button;
        if (but.Background.Equals(Brushes.LightGray))
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
        // Оновлюємо обробники перед дією


        // Виводимо повідомлення після зміни
        MessageBox.Show("I am super! :)");
    }
    private void checkbox_changed(object sender, RoutedEventArgs e)
    {
        if (first.IsChecked == true)
            super.Click += trans_click;
        else
            super.Click -= trans_click;

        if (second.IsChecked == true)
            super.Click += back_click;
        else
            super.Click -= back_click;

        if (third.IsChecked == true)
            super.Click += hello_click;
        else
            super.Click -= hello_click;
    }

}

    
   