using System.Windows;

namespace Laba1;

public partial class Second : Window
{
    private delegate int[] Change_Array(int[] array,int k);
    public Second()
    {
        InitializeComponent();
        
    }
    private void first_Click(object sender, RoutedEventArgs e)
    {
        string[] data = input.Text.Trim().Split();
        int k = int.Parse(number_k.Text);
        int[] array=new int[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            array[i] = int.Parse(data[i]);
        }

        try 
        {
            Change_Array method = Method_1;
            output.Text = string.Join(" ", method(array, k));
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }

    }

    private void second_Click(object sender, RoutedEventArgs e)
    {
        
            string[] data = input.Text.Trim().Split();
            int k = int.Parse(number_k.Text);
            int[] array = new int[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                array[i] = int.Parse(data[i]);
            }
        try 
        {
            Change_Array method = Method_2;
            output.Text = string.Join(" ", method(array, k));
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message);
        }
    }

    public int[] Method_1(int[] array,int k)
    {
        int[] array_new=array.Where(x => x % k == 0).ToArray();
        return array_new;
    }

    public int[] Method_2(int[] array, int k)
    {
        int count = 0;
        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] % k == 0)
            {
                count++;
            }
        }

        int[] result = new int[count];
        int index = 0;

        for (int i = 0; i < array.Length; i++)
        {
            if (array[i] % k == 0)
            {
                result[index] = array[i];
                index++;
            }
        }

        return result;
    }
    
}