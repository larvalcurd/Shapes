using Shapes;
using Shapes.Gfx;
using Shapes.Shapes;

namespace Shapes.Tests.TestDoubles;

public class FakeGeometry : IShapeGeometry
{
    public string TypeName => "fake";

    public int MoveCallCount { get; private set; }
    public double LastDx { get; private set; }
    public double LastDy { get; private set; }
    
    public int DrawCallCount { get; private set; }
    public Color LastDrawColor  { get; private set; }

    public void Move(double dx, double dy)
    {
        MoveCallCount++;
        LastDx = dx;
        LastDy = dy;
    }

    public void Draw(ICanvas canvas, Color color)
    {
        DrawCallCount++;
        LastDrawColor = color;
    }

    public string GetParamsString() => "fake-params";
}