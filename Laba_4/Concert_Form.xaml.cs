using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Laba_4;

public partial class Concert_Form : Window
{
    public Concert Result { get; private set; }

    private Concert_ViewModel vm;
    public Concert_Form(Concert? concert=null)
    {
        InitializeComponent();
        
        vm = concert == null 
            ? new Concert_ViewModel() 
            : new Concert_ViewModel(concert);

        vm.OnSuccess = (concert) =>
        {
            Result = concert;
            DialogResult = true;
            Close();
        };
        vm.OnAddPerformance = _ =>
        {
            var window = new Perfomance_Form();
            if (window.ShowDialog() == true)
            {
                var new_performance = window.Result;
                vm.AddPerformance(new_performance);
                MessageBox.Show("Performance додано: " + new_performance.Title);
            }
        };

        DataContext = vm;
    }

    
    // private void performance_mouse(object sender, MouseEventArgs e)
    // {
    //     if (sender is ListBoxItem item && item.DataContext is Performance the_performance)
    //     {
    //         ToolTip tooltip = new ToolTip
    //         {
    //             Content = $"Performance:\nTitle: {the_performance.Title}" +
    //                       $"\nPerformer: {the_performance.The_Performer.Name} {the_performance.The_Performer.Surname}" +
    //                       $"\nWork: {the_performance.The_Work}" +
    //                       $"\nDuration: {the_performance.Duration}"
    //         };
    //         item.ToolTip = tooltip;
    //     }
    // }

}