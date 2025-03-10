using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Controls;
using System.Windows;

namespace Laba1;

public class Timer
{
    private List<Shape> shapes = new List<Shape>();
    private static int step = 5;
    private static int rotation_angle = 20;
    private static Canvas canvas_first;
    


    private Dictionary<Type, Action<Shape>> dict = new Dictionary<Type, Action<Shape>>
    {
        { typeof(Ellipse), DrawEllipse},
        { typeof(Rectangle),DrawSquare },
        { typeof(Polygon), DrawTriangleOrStar},
    };

    public static void SetCanvas(Canvas canvas)
    {
        canvas_first = canvas;
    }
    
    public Timer(List<Shape> shapes, Canvas canvas_first)
    {
        this.shapes = shapes;
        Timer.canvas_first = canvas_first;
    }
    public void DrawShapes()
    {
        foreach (var shape in shapes)
        {
            Type shapeType = shape.GetType();
            if (dict.ContainsKey(shapeType))
            {
                dict[shapeType].Invoke(shape); // Викликаємо метод з словника
            }

        }
    }

    public async Task MoveAsync()
    {
        for (int i = 0; i < 100; i++) // Анімація 50 кроків
        {
            foreach (var shape in shapes)
            {
                Canvas.SetTop(shape, Canvas.GetTop(shape) + step);

                if (shape.RenderTransform is RotateTransform rotate)
                {
                    rotate.Angle += rotation_angle;
                }
            }
            await Task.Delay(100); 
        }
    }

    public static void DrawEllipse(Shape shape)
    {
        if (shape is Ellipse circle)
        {
            Canvas.SetLeft(circle, 50);
            Canvas.SetTop(circle, 50);
            RotateTransform rotate = new RotateTransform(0);
            circle.RenderTransform = rotate;
            circle.RenderTransformOrigin = new Point(0.5, 0.5);
            canvas_first.Children.Add(circle);
        }
    }

    public static void DrawSquare(Shape shape)
    {
        if (shape is Rectangle square)
        {
            Canvas.SetLeft(square, 150);
            Canvas.SetTop(square, 50);
            RotateTransform rotate = new RotateTransform(0);
            square.RenderTransform = rotate;
            square.RenderTransformOrigin = new Point(0.5, 0.5);
            canvas_first.Children.Add(square);
        }
    }
    public static void DrawTriangleOrStar(Shape shape)
    {
        if (shape is Polygon polygon)
        {
            if (polygon.Points.Count == 3)
            {
                Canvas.SetLeft(polygon, 250);
                Canvas.SetTop(polygon, 50);
            }
            else if (polygon.Points.Count >= 5)
            {
                Canvas.SetLeft(polygon, 350);
                Canvas.SetTop(polygon, 50);
            }

            RotateTransform rotate = new RotateTransform(0);
            polygon.RenderTransform = rotate;
            polygon.RenderTransformOrigin = new Point(0.5, 0.5);
            canvas_first.Children.Add(polygon);
        }
    }

    
    
}