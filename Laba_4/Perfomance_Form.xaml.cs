using System.Windows;

namespace Laba_4;

public partial class Perfomance_Form : Window
{
    public Performance Result { get; private set; }
    private PerformanceViewModel vm;
    public Perfomance_Form()
    {
        InitializeComponent();
        
        vm = new PerformanceViewModel();
        vm.OnSuccess = (perf) =>
        {
            Result = perf;
            DialogResult = true;
            Close();
        };
        DataContext = vm;
    }
}