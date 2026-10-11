using Shapes.Gfx;
using Shapes.Shapes;
using Shapes.Shapes.Geometries;
using Shapes.Tests.Mocks;

namespace Shapes.Tests;

public class ShapeTests
{
    [Fact]
    public void Move_ChangesCoordinates_AndPreserveShapeProperties()
    {
        var red = new Color(255, 0, 0);
        var fake = new FakeGeometry();
        var shape = new Shape("sh1", red, fake);
        var originalId = shape.Id;
        var originalColor = shape.Color;
        
        shape.Move(10, 20);
        
        Assert.Single(fake.MoveCalls);  
        Assert.Equal((10, 20), fake.MoveCalls[0]);
        Assert.Equal(originalId, shape.Id);
        Assert.Equal(originalColor, shape.Color);
    }
    
    [Fact]
    public void ReplaceGeometry_ChangesTypeName_AndPreservesIdAndColor()
    {
        var red = new Color(255, 0, 0);
        var shape = new Shape("sh1", red, new CircleGeometry(10, 20, 5));
        var rect = new RectangleGeometry(0, 0, 10, 20);
        
        var originalId = shape.Id;
        var originalColor = shape.Color;
        
        shape.ReplaceGeometry(rect);
        
        Assert.Equal("rectangle", shape.GetTypeName());
        Assert.Equal(originalId, shape.Id);
        Assert.Equal(originalColor, shape.Color);
    }

    [Fact]
    public void Draw_SetsColorAndPassesSameCanvasBeforeDrawingGeometry()
    {
        var order = new OrderCounter();
        var fakeCanvas = new FakeCanvas(order);
        var fakeGeometry = new FakeGeometry(order);
        
        var red =  new Color(255, 0, 0);
        var shape = new Shape("sh1", red, fakeGeometry);

        shape.Draw(fakeCanvas);
        
        var colorCall = Assert.Single(fakeCanvas.Calls);
        
        Assert.Equal("SetColor", colorCall.Method);
        Assert.Equal(red, (Color)colorCall.Args[0]);

        Assert.Equal(1, fakeGeometry.DrawCallCount);
        Assert.Same(fakeCanvas, fakeGeometry.LastCanvas);
        
        Assert.True(colorCall.Order < fakeGeometry.DrawOrder);
    }

    [Fact]
    public void Clone_CreatesIndependentCopy_AndPreservesOriginalProperties()
    {
        var fakeOriginal = new FakeGeometry();
        var red = new Color(255, 0, 0);
        var shape = new Shape("sh1", red, fakeOriginal);
        
        var originalId = shape.Id;
        var originalColor = shape.Color;

        var clone = shape.Clone("sh2");
        
        Assert.Equal("sh2", clone.Id);
        Assert.Equal(originalColor, clone.Color);
        Assert.Equal(originalId, shape.Id);
        Assert.Equal(1, fakeOriginal.CloneCallCount);
        
        Assert.NotNull(fakeOriginal.LastClonedResult);
        Assert.NotSame(fakeOriginal, fakeOriginal.LastClonedResult);
    }

    [Fact]
    public void Move_OnClone_DoesNotAffectOriginal()
    {
        var fakeOriginal = new FakeGeometry();
        var red = new Color(255, 0, 0);
        var shape = new Shape("sh1", red, fakeOriginal);
        
        var clone = shape.Clone("sh2");


        clone.Move(1, 2);
        
        Assert.NotNull(fakeOriginal.LastClonedResult);
        Assert.Equal((1, 2), fakeOriginal.LastClonedResult!.MoveCalls[0]);
        Assert.Empty(fakeOriginal.MoveCalls);
    }
}