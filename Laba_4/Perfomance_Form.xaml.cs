using System.Windows;

namespace Laba_4;

public partial class Perfomance_Form : Window
{
    public Performance Result { get; private set; }
    private PerformanceViewModel vm;
    public Perfomance_Form(Performance? performance = null)
    {
        InitializeComponent();

        vm = performance==null ? new PerformanceViewModel() : new PerformanceViewModel(performance);
        vm.OnSuccess = (perf) =>
        {
            Result = perf;
            DialogResult = true;
            Close();
        };
        DataContext = vm;
    }
}