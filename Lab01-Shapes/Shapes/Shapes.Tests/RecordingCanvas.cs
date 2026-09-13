using Shapes.Gfx;

namespace Shapes.Tests;

internal sealed class RecordingCanvas : ICanvas
{
    public List<string> Calls { get; } = [];

    public void SetColor(Color color) => Calls.Add($"color:{color}");
    public void MoveTo(double x, double y) => Calls.Add($"move:{x},{y}");
    public void LineTo(double x, double y) => Calls.Add($"line:{x},{y}");
    public void DrawEllipse(double cx, double cy, double rx, double ry) =>
        Calls.Add($"ellipse:{cx},{cy},{rx},{ry}");
    public void DrawText(double left, double top, double fontSize, string text) =>
        Calls.Add($"text:{left},{top},{fontSize},{text}");
}
