using Shapes.Gfx;
using Shapes.Shapes;

namespace Shapes.Tests.Mocks;

public class FakeGeometry(OrderCounter? orderCounter = null) : IShapeGeometry
{
    private readonly List<(double Dx, double Dy)> _moveCalls = [];

    public IReadOnlyList<(double Dx, double Dy)> MoveCalls => [.. _moveCalls];

    public int DrawCallCount { get; private set; }

    public int DrawOrder { get; private set; } = -1;

    public ICanvas? LastCanvas { get; private set; }
    
    public FakeGeometry? LastClonedResult { get; private set; }
    
    public int CloneCallCount { get; private set; }

    public string TypeName => "FakeGeometry";

    public void Move(double dx, double dy) => _moveCalls.Add((dx, dy));

    public void Draw(ICanvas canvas)
    {
        ArgumentNullException.ThrowIfNull(canvas);

        DrawCallCount++;
        DrawOrder = orderCounter?.Next() ?? -1;
        LastCanvas = canvas;
    }

    public string GetParamsString() => throw new NotImplementedException();

    public IShapeGeometry Clone()
    {
        CloneCallCount++;
        
        var clone = new FakeGeometry(orderCounter);
        LastClonedResult = clone;

        return LastClonedResult;
    }
}