using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Laba4_Core.Class;
using Laba4_Core.DTO;
using Laba4_Core.Interface;
using Laba4_Core.ViewModel;

namespace Laba_4;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    
    private MainViewModel vm;
    CommandFactory commandFactory;    
    public MainWindow()
    {
        InitializeComponent();
        CommandFactory factory = new CommandFactory((exec, сanc) =>new RelayCommand(exec));
        vm= new MainViewModel(factory);
        var navigationService=new WPFNavigationService();
        vm.SetNavigationService(navigationService);
        vm.ShowMessage = message => MessageBox.Show(message);
        DataContext = vm;
    }

    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);
        vm.SaveConcertsToFile();
    }


}