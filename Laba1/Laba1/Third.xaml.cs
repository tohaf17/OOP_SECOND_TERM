using System.Globalization;
using System.Windows;

namespace Laba1;

public partial class Third : Window
{
    
    public Third()
    {
        InitializeComponent();
    }

    public void submit_button(object sender, RoutedEventArgs e)
    {
        one.Text = SumElementsOfRange(Range_1,2).ToString();
        two.Text= SumElementsOfRange(Range_2,Double.E-1).ToString();
        three.Text= SumElementsOfRange(Range_3,-2/3.0).ToString();
    }
    private delegate double SumOfRange(int n);
    private (double,int) SumElementsOfRange(SumOfRange formula,double correct)
    {
        double epsilon = double.Parse(precision.Text, CultureInfo.InvariantCulture);
        double sum = 0.0;
        int n = 1;
        while (true)
        {
            double step = formula(n);
            if (Math.Abs(correct-sum) < epsilon)
            {
                break;
            }

            sum += step;
            n++;
        }

        return (sum,n);
        
    }
    public static double Range_1(int n) => 1.0 / Math.Pow(2, n - 1);
    public static double Factorial(int n) => n <= 1 ? 1 : n * Factorial(n - 1);
    public static double Range_2(int n) => 1.0 / Factorial(n);
    public static double Range_3(int n) => Math.Pow(-1,n)/Math.Pow(2,n-1);
}