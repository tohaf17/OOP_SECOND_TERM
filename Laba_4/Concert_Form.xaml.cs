using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;

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
        vm.OnEditPerformance = _ =>
        {
            if (performancesGrid.SelectedItem is not Performance selected_performance)
            {
                MessageBox.Show("Please select a concert");
                return;
            }

            var window = new Perfomance_Form(selected_performance);


            bool? result = window.ShowDialog();

            if (result == true)
            {
                Performance updatedConcert = window.Result;

                selected_performance.The_Work = updatedConcert.The_Work;
                selected_performance.The_Performer = updatedConcert.The_Performer;
                selected_performance.Title = updatedConcert.Title;
                selected_performance.Duration = updatedConcert.Duration;
                

                //CollectionViewSource.GetDefaultView(Performances).Refresh();
            }

        };

        DataContext = vm;
    }

    
    

}