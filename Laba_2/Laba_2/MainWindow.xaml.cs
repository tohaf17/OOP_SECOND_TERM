using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Globalization;

namespace Laba_2;
public partial class MainWindow : Window
{
    private Calculator calculator;
    private Manager manager;
    private bool is_extra = false;

    private string display_number = "";
    private string calculate_number = "";

    public MainWindow()
    {
        InitializeComponent();
        calculator = new Calculator();
        manager = new Manager();
        KeyDown += main_window_key_down;
        update_display();
    }

    private void main_window_key_down(object sender, KeyEventArgs e) {
            switch (e.Key) {
                case var k when k==Key.D1 || k==Key.NumPad1:
                    process_number("1");
                    break;
                case var k when k==Key.D2 || k==Key.NumPad2:
                    process_number("2");
                    break;
                case var k when k==Key.D3 || k==Key.NumPad3:
                    process_number("3");
                    break;
                case var k when k==Key.D4 || k==Key.NumPad4:
                    process_number("4");
                    break;
                case var k when k==Key.D5 || k==Key.NumPad5:
                    process_number("5");
                    break;
                case var k when k==Key.D6 || k==Key.NumPad6:
                    process_number("6");
                    break;
                case var k when k==Key.D7 || k==Key.NumPad7:
                    process_number("7");
                    break;
                case var k when k==Key.D8 || k==Key.NumPad8:
                    process_number("8");
                    break;
                case var k when k==Key.D9 || k==Key.NumPad9:
                    process_number("9");
                    break;
                case var k when k==Key.D0 || k==Key.NumPad0:
                    process_number("0");
                    break;
                case Key.Add:
                    process_operator("+");
                    break;
                case Key.Subtract:
                    process_operator("-");
                    break;
                case Key.Divide:
                    process_operator("/");
                    break;
                case Key.Multiply:
                    process_operator("*");
                    break;
                case Key.Enter:
                    calculate_result();
                    break;
                case Key.Escape:
                    Clear_Click(null, null);
                    break;
                case Key.Back:
                    back_method(null, null);
                    break;
                case Key.Decimal:
                case Key.OemPeriod:
                case Key.OemComma:
                    Decimal_Click(null, null);
                    break;
            }
        }
    private void ce_method(object sender, RoutedEventArgs e) {
        manager.Undo();
        update_display();
    }
    private void equal_method(object sender, RoutedEventArgs e) {
        calculate_result();
    }
    private void method_numbers(object sender, RoutedEventArgs e) {
        string number = ((Button)sender).Content.ToString();
        process_number(number);
    }
    private void Decimal_Click(object sender, RoutedEventArgs e) {
        if (calculator.is_error)
            calculator.restart_error();

        var command = new Decimal_Command(calculator);
        manager.Execute(command);
        update_display();
    }
    private void Operator_Click(object sender, RoutedEventArgs e) {
        string op = ((Button)sender).Content.ToString();
        process_operator(op);
    }

    private void Clear_Click(object sender, RoutedEventArgs e) {
        var command = new Clear_Command(calculator);
        manager.Execute(command);
        update_display();
    }

    private void back_method(object sender, RoutedEventArgs e) {
        if (calculator.is_error) {
            calculator.restart_error();
            update_display();
            return;
        }

        if (!calculator.is_new && calculator.display.Length > 0) {
            var command = new Back_Command(calculator);
            manager.Execute(command);
            update_display();
        }
    }
    private void process_number(string number)
    {
        if (calculator.is_error)
        {
            calculator.restart_error();
        }
        var command=new Input_Command(calculator, number);
        manager.Execute(command);
        update_display();
    }
    private void process_operator(string op) {
        if (calculator.is_error)
            calculator.restart_error();

        try {
            if (!string.IsNullOrEmpty(calculator.calculate) && !calculator.is_new)
                calculate_result();
                
            var command = new Operator_Command(calculator, op);
            manager.Execute(command);
            update_display();
        } catch {
            calculator.Set_is_error();
            update_display();
        }
    }
    private void calculate_result() {
        if (calculator.is_error || string.IsNullOrEmpty(calculator.calculate) || calculator.is_new)
            return;

        try {
            var command = new Calculate_Command(calculator);
            manager.Execute(command);
            update_display();
        } catch {
            calculator.Set_is_error();
            update_display();
        }
    }
    private void update_display()
    {
        display.Text = calculator.display;
        calculate.Text = calculator.calculate;

    }
    private void extra_column_click(object sender, RoutedEventArgs e) {
        is_extra = !is_extra;
            
        if (is_extra) {
            extra_column.Width = new GridLength(1, GridUnitType.Star);
            Width += 80;
            menu_button.Visibility = Visibility.Collapsed;
            collapse_button.Visibility = Visibility.Visible;
                
            foreach (UIElement element in buttons.Children) {
                if (Grid.GetColumn(element) == 4)
                    element.Visibility = Visibility.Visible;
            }
        } else {
            extra_column.Width = new GridLength(0);
            Width -= 80;
            menu_button.Visibility = Visibility.Visible;
            collapse_button.Visibility = Visibility.Collapsed;
                
            foreach (UIElement element in buttons.Children) {
                if (Grid.GetColumn(element) == 4)
                    element.Visibility = Visibility.Collapsed;
            }
        }
    }
    private void extra_method(object sender, RoutedEventArgs e) {
        if (calculator.is_error) {
            calculator.restart_error();
            update_display();
        }

        string operation = ((Button)sender).Content.ToString();
        try {
            var command = new Extra_Command(calculator, operation);
            manager.Execute(command);
            update_display();
        } catch {
            calculator.Set_is_error();
            update_display();
        }
    }

   

    

}
