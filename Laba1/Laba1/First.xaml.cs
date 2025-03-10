using System.Windows;

using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;

namespace Laba1;

public partial class First : Window
{
    public First()
    {
        InitializeComponent();
        
    }
    
    public async void Confirm_Click(object sender, EventArgs e)
    {
        Panel.Visibility = Visibility.Collapsed;
        canvas_first.Visibility = Visibility.Visible;

        List<Shape> shapes = new List<Shape>();

        if (Ellipse.IsChecked == true)
        {
            shapes.Add(new Ellipse { Width = 50, Height = 50, Fill = Brushes.Crimson });
        }
        if (Square.IsChecked == true)
        {
            shapes.Add(new Rectangle { Width = 50, Height = 50, Fill = Brushes.YellowGreen });
        }
        if (Triangle.IsChecked == true)
        {
            shapes.Add(new Polygon
            {
                Points = new PointCollection
                {
                    new Point(0, 50), new Point(25, 0), new Point(50, 50)
                },
                Fill = Brushes.Purple
            });
        }
        if (Star.IsChecked == true)
        {
            shapes.Add(new Polygon
            {
                Points = new PointCollection
                {
                    new Point(25, 0),
                    new Point(32, 18),
                    new Point(50, 18),
                    new Point(36, 30),
                    new Point(42, 50),
                    new Point(25, 38),
                    new Point(8, 50),
                    new Point(14, 30),
                    new Point(0, 18),
                    new Point(18, 18)
                },
                Fill = Brushes.Gold
            });
        }

        if (shapes.Count > 0)
        {
            Timer.SetCanvas(canvas_first);
            Timer timer = new Timer(shapes, canvas_first);
            timer.DrawShapes();
            await timer.MoveAsync();
        }
    }

        
        
}
