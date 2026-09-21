using Shapes.Gfx;
using Shapes.Shapes;
using Shapes.Shapes.Geometries;

namespace Shapes.Tests;

public class ShapeCloneTests
{
    [Fact]
    public void Clone_CreatesIndependentShapeWithNewId()
    {
        var original = new Shape("sh1", Color.Parse("#ff0000"), new CircleGeometry(0, 0, 5));

        var clone = original.Clone("sh2");
        
        Assert.Equal("sh2", clone.Id);
        Assert.Equal(original.Color, clone.Color);
        Assert.NotSame(original.Geometry, clone.Geometry);
    }

    [Fact]
    public void Clone_MovingCloneDoesNotAffectOriginal()
    {
        var original = new Shape("sh1", Color.Parse("#ff0000"), new CircleGeometry(0, 0, 5));
        var clone = original.Clone("sh2");
        
        clone.Move(10, 10);
        clone.Color = Color.Parse("#00ff00");
        
        Assert.Equal("0 0 5", original.Geometry.GetParamsString());
        Assert.Equal("10 10 5", clone.Geometry.GetParamsString());
        Assert.Equal(Color.Parse("#ff0000"), original.Color);
    }
}

public class PictureCloneTests
{
    [Fact]
    public void PictureClone_IsIndependentFromOriginal()
    {
        var picture = new Picture();
        picture.AddShape(new Shape("a", Color.Parse("#000000"), new CircleGeometry(0, 0, 1)));
        
        var clone = picture.Clone();
        clone.GetShape("a").Move(10, 10);
        clone.DeleteShape("a");
        
        Assert.Equal("0 0 1", picture.GetShape("a").Geometry.GetParamsString());
        Assert.Single(picture.GetShapesInOrder());
        
        Assert.Empty(clone.GetShapesInOrder());
    }

    [Fact]
    public void Clone_ThenAddWithExistingId_ThrowsAndDoesNotModifyPicture()
    {
        var picture = new Picture();
        picture.AddShape(new Shape("a", Color.Parse("#000000"), new CircleGeometry(0,0,1)));
        picture.AddShape(new Shape("b", Color.Parse("#000000"), new CircleGeometry(1,1,1)));

        var original = picture.GetShape("a");
        var clone = original.Clone("b");
        
        Assert.Throws<InvalidOperationException>(() => picture.AddShape(clone));
        Assert.Equal(2, picture.GetShapesInOrder().Count);
    }
}