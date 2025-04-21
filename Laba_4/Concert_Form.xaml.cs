using System.Windows;

namespace Laba_4;

public partial class Concert_Form : Window
{
    public Concert Result { get; private set; }

    private Concert_ViewModel vm;
    public Concert_Form()
    {
        InitializeComponent();
        vm=new Concert_ViewModel();
        DataContext = vm;
    }

    public void add_performance(object sender, RoutedEventArgs e)
    {
        var window = new Perfomance_Form();
        bool? result = window.ShowDialog();

        if (result == true)
        {
            Performance new_performance = window.Result;
            vm.AddPerformance(new_performance); 
            MessageBox.Show("Performance додано: " + new_performance.Title);
        }
    }

    public void ok_click(object sender, RoutedEventArgs e)
    {
        Result = vm.BuildConcert();
        DialogResult = true;
        Close();
    }

}