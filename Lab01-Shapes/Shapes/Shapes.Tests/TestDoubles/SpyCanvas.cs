using Shapes.Gfx;

namespace Shapes.Tests.TestDoubles;

public class SpyCanvas : ICanvas
{
    public List<string> Calls { get; } = [];
    
    public void SetColor(Color color) => Calls.Add($"SetColor({color})");
    public void MoveTo(double x, double y) => Calls.Add($"MoveTo({x},{y})");
    public void LineTo(double x, double y) => Calls.Add($"LineTo({x},{y})");
    public void DrawEllipse(double cx, double cy, double rx, double ry) =>
        Calls.Add($"DrawEllipse({cx},{cy},{rx},{ry})");
    public void DrawText(double left, double top, double fontSize, string text) =>
        Calls.Add($"DrawText({left},{top},{fontSize},{text})");
}