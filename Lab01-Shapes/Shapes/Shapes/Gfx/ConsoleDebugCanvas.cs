namespace Shapes.Gfx;

public class ConsoleDebugCanvas : ICanvas
{
    public void SetColor(Color color) => Console.WriteLine($"SetColor {color}");
    public void MoveTo(double x, double y) => Console.WriteLine($"MoveTo {x} {y}");
    public void LineTo(double x, double y) => Console.WriteLine($"LineTo {x} {y}");
    public void DrawEllipse(double cx, double cy, double rx, double ry) =>
        Console.WriteLine($"DrawEllipse {cx} {cy} {rx} {ry}");
    public void DrawText(double left, double top, double fontSize, string text) =>
        Console.WriteLine($"DrawText {left} {top} {fontSize} {text}");
}