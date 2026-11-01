using Shapes.Core;
using Shapes.Core.Listeners;

namespace Shapes.Tests.Mocks;

public class FakeShapeListener : IShapeListener
{
    public int CallCount { get; private set; }
    public Shape? LastNotifiedShape  { get; private set; }

    public void OnShapeChanged(Shape shape)
    {
        CallCount++;
        LastNotifiedShape = shape;
    }
}