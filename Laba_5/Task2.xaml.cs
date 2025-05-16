using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Laba_5;
/// <summary>
/// Interaction logic for Task2.xaml
/// </summary>
public partial class Task2 : Window
{
    public ObservableCollection<PointViewModel> Points { get; } = new();
    public PointViewModel? selectedPoint; // обраний елемент
    public Task2()
    {
        InitializeComponent();
        DataContext = this;
        this.KeyDown += Task2_KeyDown; 
        this.Focusable = true;
        this.Focus();
    }
    private void Generate_Points(object sender, RoutedEventArgs e)
    {
        Random random = new Random();
        int generated = 0;
        int attempts = 0;

        while (generated < 50 && attempts < 10000)
        {
            double x = random.Next(0, (int)myCanvas.ActualWidth - 3);
            double y = random.Next(0, (int)myCanvas.ActualHeight - 3);

            bool tooClose = false;
            foreach (var point in Points)
            {
                double dx = point.X - (x - 3);
                double dy = point.Y - (y - 3);
                double distSq = dx * dx + dy * dy;
                if (distSq < 100)
                {
                    tooClose = true;
                    break;
                }
            }

            if (!tooClose)
            {
                Points.Add(new PointViewModel { X = x - 3, Y = y - 3 });
                generated++;
            }

            attempts++;
        }
    }

    private void MouseClick(object sender, MouseButtonEventArgs e)
    {
        var current = e.GetPosition((IInputElement)sender);
        foreach (var point in Points)
        {
            double dx = point.X - (current.X - 3);
            double dy = point.Y - (current.Y - 3);
            if (dx * dx + dy * dy < 9)
            {
                point.isSelected = true;
                return;
            }
            if (dx * dx + dy * dy < 100)
            {
                MessageBox.Show("Distance between points must be greater than 10px");
                return;
            }
        }

        Points.Add(new PointViewModel { X = current.X - 3, Y = current.Y - 3 });
    }

    private void Task2_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Delete && selectedPoint != null)
        {
            Points.Remove(selectedPoint);
            selectedPoint = null;
        }
    }

    private void DeleteSmallestAreas(object sender, RoutedEventArgs e)
    {
        var pixelCounts = Points.ToDictionary(p => p, _ => 0);
        var visualizer = new Algorithm(VoronoiImage, Points.ToList(), false, comboMetricks.SelectedIndex, isShowInformation: false);
        visualizer.Run();

        var ordered = pixelCounts.OrderBy(kv => kv.Value).ToList();
        int toRemove = (int)(ordered.Count * 0.2);
        for (int i = 0; i < toRemove; i++)
        {
            Points.Remove(ordered[i].Key);
        }
        var visualizerAfter = new Algorithm(VoronoiImage, Points.ToList(), true, comboMetricks.SelectedIndex);
        visualizerAfter.Run();
    }

    private void DeletePoints(object sender, RoutedEventArgs e)
    {
        Random random = new Random();
        var indicesToRemove = new HashSet<int>();

        int countToRemove = Math.Min(50, Points.Count);
        while (indicesToRemove.Count < countToRemove)
        {
            indicesToRemove.Add(random.Next(Points.Count));
        }

        foreach (int index in indicesToRemove.OrderByDescending(i => i))
        {
            Points.RemoveAt(index);
        }
    }
    private void DrawVoronoi_Click(object sender, RoutedEventArgs e)
    {
        bool useParallel = comboBox.SelectedIndex == 1;
        var visualizer = new Algorithm(VoronoiImage, Points.ToList(), useParallel, comboMetricks.SelectedIndex);
        visualizer.Run();
    }

    private void Ellipse_Click(object sender, MouseButtonEventArgs e)
    {
        var ellipse = sender as Ellipse;
        var point = ellipse?.DataContext as PointViewModel;
        if (point != null)
        {
            foreach (var p in Points)
                p.isSelected = false;

            point.isSelected = true;
            selectedPoint = point;
        }
    }
}

public class PointViewModel
{
    public double X { get; set; }
    public double Y { get; set; }
    public bool isSelected { get; set; }
    public bool isGone { get; set; } = false;
}

public class BoolToBrushConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        return (bool)value ? Brushes.Lime : Brushes.Red;
    }

    public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}

public class Algorithm
{
    private readonly Image imageControl;
    private readonly List<PointViewModel> points;
    private readonly Random random = new();
    private WriteableBitmap bmp;
    private int width;
    private int height;
    private int stride;
    private byte[] pixels;
    private readonly bool IsParalel;
    private readonly Dictionary<PointViewModel, int>? pixelCounter;
    private bool isShowInformation;
    private int index;

    public Algorithm(Image targetImage, List<PointViewModel> inputSites, bool isParalel, int index, Dictionary<PointViewModel, int>? counter = null,bool isShowInformation=true)
    {
        imageControl = targetImage;
        points = inputSites;
        IsParalel = isParalel;
        pixelCounter = counter;
        this.isShowInformation = isShowInformation;
        this.index= index;
    }

    public void Run()
    {
        width = (int)((FrameworkElement)imageControl.Parent).ActualWidth;
        height = (int)((FrameworkElement)imageControl.Parent).ActualHeight;
        if (width == 0 || height == 0) return;

        bmp = new WriteableBitmap(width, height, 96, 96, PixelFormats.Bgra32, null);
        stride = width * 4;
        pixels = new byte[height * stride];
        Array.Fill(pixels, (byte)255);

        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        long cpuStart = Process.GetCurrentProcess().TotalProcessorTime.Ticks;
        long memStart = GC.GetTotalMemory(true);

        if (IsParalel)
        {
            Parallel.For(0, width, x =>
            {
                for (int y = 0; y < height; y++)
                    Checking(x, y);
            });
        }
        else
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                    Checking(x, y);
            }
        }

        bmp.WritePixels(new Int32Rect(0, 0, width, height), pixels, stride, 0);
        imageControl.Source = bmp;

        stopwatch.Stop();
        long cpuEnd = Process.GetCurrentProcess().TotalProcessorTime.Ticks;
        long memEnd = GC.GetTotalMemory(false);

        double realTime = stopwatch.Elapsed.TotalMilliseconds;
        double cpuTime = TimeSpan.FromTicks(cpuEnd - cpuStart).TotalMilliseconds;
        long usedMemory = memEnd - memStart;

        if (isShowInformation)
        {
            MessageBox.Show(
                $"Real time: {realTime:F2} ms\n" +
                $"CPU time: {cpuTime:F2} ms\n" +
                $"Used memory: {usedMemory / 1024.0:F2} KB",
                "Performance");
        }
    }

    private void Checking(int x, int y)
    {
        int closest = -1;
        double minDist = double.MaxValue;

        double px = width*0.3;
        double py = height*0.4;

        for (int i = 0; i < points.Count; i++)
        {
            if (points[i].isGone) continue;
            double dx = x - points[i].X;
            double dy = y - points[i].Y;

            double dist = index switch
            {
                0 => dx * dx + dy * dy,
                1 => Math.Abs(dx) + Math.Abs(dy),
                2 => Math.Max(Math.Abs(dx), Math.Abs(dy)),
                3=>Math.Abs(dx*dx*dx)+Math.Abs(dy*dy*dy),
                4=>Math.Pow(dx,100)+ Math.Pow(dy, 100),
                _ => dx * dx + dy * dy
            };

            if (dist < minDist)
            {
                minDist = dist;
                closest = i;
            }
        }

        if (closest >= 0)
        {
            if (pixelCounter != null)
            {
                pixelCounter[points[closest]] =
                    (pixelCounter.TryGetValue(points[closest], out int count) ? count : 0) + 1;
            }

            int indexPixel = y * stride + x * 4;
            var color = GetColorByIndex(closest);
            pixels[indexPixel + 0] = color.B;
            pixels[indexPixel + 1] = color.G;
            pixels[indexPixel + 2] = color.R;
            pixels[indexPixel + 3] = 255;
        }
    }

    private bool AreColinear(double x1, double y1, double x2, double y2, double px, double py)
    {
        return Math.Abs((x1 - px) * (y2 - py) - (x2 - px) * (y1 - py)) < 1e-2;
    }

    private Color GetColorByIndex(int i)
    {
        var rnd = new Random(i * 1000);
        return Color.FromRgb((byte)rnd.Next(60, 256), (byte)rnd.Next(60, 256), (byte)rnd.Next(60, 256));
    }
}

