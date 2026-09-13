using Shapes;
using Shapes.Shapes;
using Shapes.Gfx;
using Shapes.Tests.TestDoubles;
using Xunit;

namespace Shapes.Tests;

public class ShapeTests
{
    [Fact]
    public void Constructor_SetsIdColorAndGeometry()
    {
        var geometry = new FakeGeometry();
        var color = Color.Parse("#ff0000");

        var shape = new Shape("sh1", color, geometry);

        Assert.Equal("sh1", shape.Id);
        Assert.Equal(color, shape.Color);
        Assert.Same(geometry, shape.Geometry);
    }

    [Fact]
    public void Move_DelegatesToGeometryWithSameArguments()
    {
        var geometry = new FakeGeometry();
        var shape = new Shape("sh1", Color.Parse("#ff0000"), geometry);

        shape.Move(3, -4);

        Assert.Equal(1, geometry.MoveCallCount);
        Assert.Equal(3, geometry.LastDx);
        Assert.Equal(-4, geometry.LastDy);
    }

    [Fact]
    public void Draw_DelegatesToGeometryWithCurrentColor()
    {
        var geometry = new FakeGeometry();
        var color = Color.Parse("#00ff00");
        var shape = new Shape("sh1", color, geometry);
        var canvas = new SpyCanvas();

        shape.Draw(canvas);

        Assert.Equal(1, geometry.DrawCallCount);
        Assert.Equal(color, geometry.LastDrawColor);
    }

    [Fact]
    public void Draw_UsesUpdatedColorAfterColorChanged()
    {
        var geometry = new FakeGeometry();
        var shape = new Shape("sh1", Color.Parse("#000000"), geometry);
        var canvas = new SpyCanvas();

        shape.Color = Color.Parse("#ffffff");
        shape.Draw(canvas);

        Assert.Equal(Color.Parse("#ffffff"), geometry.LastDrawColor);
    }

    [Fact]
    public void ChangingGeometry_KeepsIdAndDelegatesToNewGeometry()
    {
        var oldGeometry = new FakeGeometry();
        var shape = new Shape("sh1", Color.Parse("#ff0000"), oldGeometry);

        var newGeometry = new FakeGeometry();
        shape.Geometry = newGeometry;
        shape.Move(1, 1);

        Assert.Equal("sh1", shape.Id);
        Assert.Equal(0, oldGeometry.MoveCallCount);
        Assert.Equal(1, newGeometry.MoveCallCount);
    }
}