using System.Windows;
using Laba4_Core.Class;
using Laba4_Core.DTO;
using Laba4_Core.Interface;
using Laba4_Core.ViewModel;


namespace Laba_4;

public partial class Perfomance_Form : Window
{
    public Performance Result { get; private set; }
    private PerformanceViewModel vm;
    public Perfomance_Form(Performance? performance = null)
    {
        InitializeComponent();
        CommandFactory commandFactory = new CommandFactory((exec, can) => new RelayCommand(exec));
        vm = new PerformanceViewModel(commandFactory, performance);
        var navigationService = new WPFNavigationService();
        vm.SetNavigationService(navigationService);
        vm.OnSuccess = (perf) =>
        {
            Result = perf;
            DialogResult = true;
            Close();
        };
        vm.ShowMessage = (message)=> MessageBox.Show(message);
        DataContext = vm;
    }
}