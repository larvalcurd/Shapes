using Shapes.Gfx;
using Shapes.Core;
using Shapes.Tests.Mocks;

namespace Shapes.Tests;

public class ShapeObserverTests
{
    [Fact]
    public void Move_NotifiesListener()
    {
        var geometry = new FakeGeometry();
        var red = new Color(255, 0, 0);
        var shape = new Shape("sh1", red, geometry);
        var listener = new FakeShapeListener();

        shape.AddListener(listener);
        shape.Move(10, 0);

        Assert.Equal(1, listener.CallCount);
        Assert.Same(shape, listener.LastNotifiedShape);
    }

    [Fact]
    public void ColorChange_NotifiesListener()
    {
        var geometry = new FakeGeometry();
        var red = new Color(255, 0, 0);
        var green = new Color(0, 255, 0);
        var shape = new Shape("sh1", red, geometry);
        var listener = new FakeShapeListener();

        shape.AddListener(listener);
        shape.Color = green;

        Assert.Equal(1, listener.CallCount);
        Assert.Same(shape, listener.LastNotifiedShape);
    }

    [Fact]
    public void ReplaceGeometry_NotifiesListener()
    {
        var oldGeometry = new FakeGeometry();
        var newGeometry = new FakeGeometry();
        var red = new Color(255, 0, 0);
        var shape = new Shape("sh1", red, oldGeometry);
        var listener = new FakeShapeListener();

        shape.AddListener(listener);
        shape.ReplaceGeometry(newGeometry);

        Assert.Equal(1, listener.CallCount);
        Assert.Same(shape, listener.LastNotifiedShape);
    }

    [Fact]
    public void AddListener_Twice_DoesNotDuplicateNotifications()
    {
        var geometry = new FakeGeometry();
        var red = new Color(255, 0, 0);
        var shape = new Shape("sh1", red, geometry);
        var listener = new FakeShapeListener();

        shape.AddListener(listener);
        shape.AddListener(listener);
        shape.Move(10, 0);

        Assert.Equal(1, listener.CallCount);
    }
}
