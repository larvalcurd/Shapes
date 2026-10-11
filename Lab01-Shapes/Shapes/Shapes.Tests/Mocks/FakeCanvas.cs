using Shapes.Gfx;

namespace Shapes.Tests.Mocks;

public record CanvasCall(int Order, string Method, object[] Args);

public class FakeCanvas(OrderCounter? orderCounter = null) : ICanvas
{
    private readonly List<CanvasCall> _calls = [];
    public IReadOnlyList<CanvasCall> Calls => _calls;


    public void SetColor(Color color)
    {
        _calls.Add(new CanvasCall(orderCounter?.Next() ?? -1, "SetColor", [color]));
    }

    public void MoveTo(double x, double y)
    {
        _calls.Add(new CanvasCall(orderCounter?.Next() ?? -1, "MoveTo", [x, y]));
    }

    public void LineTo(double x, double y)
    {
        _calls.Add(new CanvasCall(orderCounter?.Next() ?? -1, "LineTo", [x, y]));
    }

    public void DrawEllipse(double cx, double cy, double rx, double ry)
    {
        _calls.Add(new CanvasCall(orderCounter?.Next() ?? -1, "DrawEllipse", [cx, cy, rx, ry]));
    }

    public void DrawText(double left, double top, double fontSize, string text)
    {
        _calls.Add(new CanvasCall(orderCounter?.Next() ?? -1, "DrawText", [left, top, fontSize, text]));
    }
}