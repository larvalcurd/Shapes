using Shapes.Gfx;
using Shapes.Core;
using Shapes.Core.Geometries;

namespace Shapes.Tests;

public class PictureTests
{
    [Fact]
    public void AddShape_DuplicateId_ThrowsInvalidOperationException()
    {
        var picture = new Picture();
        var red = new Color(255, 0, 0);
        var blue = new Color(0, 0, 255);

        var shape1 = new Shape("sh1", red, new CircleGeometry(10, 20, 5));

        var duplicate = new Shape("sh1", blue, new RectangleGeometry(0, 0, 10, 10));

        picture.AddShape(shape1);
        
        Assert.Throws<InvalidOperationException>(() => picture.AddShape(duplicate));
        
        Assert.Single(picture.GetShapes());
        Assert.Same(shape1, picture.GetShapes()[0]);
    }
}