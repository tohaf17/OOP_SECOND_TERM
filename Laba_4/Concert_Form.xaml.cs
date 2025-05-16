using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Data;
using Laba4_Core.Class;
using Laba4_Core.DTO;
using Laba4_Core.Interface;
using Laba4_Core.ViewModel;

namespace Laba_4;

public partial class Concert_Form : Window
{
    public Concert Result { get; private set; }

    private readonly Concert_ViewModel vm;
    public Concert_Form(Concert? concert=null)
    {
        InitializeComponent();
        CommandFactory commandFactory = new CommandFactory((exec, can) => new RelayCommand(exec));
        vm = new Concert_ViewModel(commandFactory,concert);
        var navigationService = new WPFNavigationService();
        vm.SetNavigationService(navigationService);

        vm.ShowMessage = message => MessageBox.Show(message);
        vm.OnSuccess = (concert) =>
        {
            Result = concert;
            DialogResult = true;
            Close();
        };
        DataContext = vm;
    }

    
    

}