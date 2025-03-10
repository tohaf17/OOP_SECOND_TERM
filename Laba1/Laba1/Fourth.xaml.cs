using System.Windows;
using System.Diagnostics;

namespace Laba1;

public partial class Fourth : Window
{
    delegate double my_delegate(double num);
    public Fourth()
    {
        InitializeComponent();
        
    }


    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        my_delegate[] array = 
        {
            x => Math.Sqrt(Math.Abs(x)),
            x => Math.Pow(x, 3.0),
            x => x + 3.5
        };
         
            try
            {
                string[] data = input.Text.Trim().Split();
                output.Text = array[int.Parse(data[0])](double.Parse(data[1])).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Сталася помилка {ex.Message}");
                
            }
        
    }
}
