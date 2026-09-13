using Shapes.Gfx;
using Shapes.Shapes;
using Shapes.Tests.TestDoubles;

namespace Shapes.Tests;

public class PictureTests
{
    private static Shape MakeShape(string id) =>
        new Shape(id, Color.Parse("#000000"), new FakeGeometry());

    [Fact]
    public void AddShape_ThenGetShape_ReturnsSameInstance()
    {
        var picture = new Picture();
        var shape = MakeShape("sh1");

        picture.AddShape(shape);

        Assert.Same(shape, picture.GetShape("sh1"));
    }

    [Fact]
    public void AddShape_DuplicateId_Throws()
    {
        var picture = new Picture();
        picture.AddShape(MakeShape("sh1"));

        Assert.Throws<InvalidOperationException>(() =>
            picture.AddShape(MakeShape("sh1")));
    }

    [Fact]
    public void GetShape_UnknownId_Throws()
    {
        var picture = new Picture();

        Assert.Throws<InvalidOperationException>(() =>
            picture.GetShape("unknown"));
    }

    [Fact]
    public void DeleteShape_UnknownId_Throws()
    {
        var picture = new Picture();

        Assert.Throws<InvalidOperationException>(() =>
            picture.DeleteShape("unknown"));
    }

    [Fact]
    public void GetShapesInOrder_ReturnsShapesInInsertionOrder()
    {
        var picture = new Picture();
        var a = MakeShape("a");
        var b = MakeShape("b");
        var c = MakeShape("c");

        picture.AddShape(a);
        picture.AddShape(b);
        picture.AddShape(c);

        var order = picture.GetShapesInOrder();

        Assert.Equal([a, b, c], order);
    }

    [Fact]
    public void DeleteShape_RemovesFromOrderButKeepsNeighborReferencesValid()
    {
        var picture = new Picture();
        var a = MakeShape("a");
        var b = MakeShape("b");
        var c = MakeShape("c");
        picture.AddShape(a);
        picture.AddShape(b);
        picture.AddShape(c);

        var referenceToB = picture.GetShape("b");

        picture.DeleteShape("a");
        picture.DeleteShape("c");
        
        Assert.Same(b, referenceToB);
        Assert.Equal("b", referenceToB.Id);

        var order = picture.GetShapesInOrder();
        Assert.Single(order);
        Assert.Same(b, order[0]);
    }

    [Fact]
    public void Move_MovesAllShapes()
    {
        var picture = new Picture();
        var g1 = new FakeGeometry();
        var g2 = new FakeGeometry();
        picture.AddShape(new Shape("a", Color.Parse("#000000"), g1));
        picture.AddShape(new Shape("b", Color.Parse("#000000"), g2));

        picture.Move(5, 7);

        Assert.Equal(1, g1.MoveCallCount);
        Assert.Equal(5, g1.LastDx);
        Assert.Equal(7, g1.LastDy);
        Assert.Equal(1, g2.MoveCallCount);
        Assert.Equal(5, g2.LastDx);
        Assert.Equal(7, g2.LastDy);
    }

    [Fact]
    public void Draw_DrawsAllShapesInOrder()
    {
        var picture = new Picture();
        var g1 = new FakeGeometry();
        var g2 = new FakeGeometry();
        picture.AddShape(new Shape("a", Color.Parse("#000000"), g1));
        picture.AddShape(new Shape("b", Color.Parse("#000000"), g2));

        var canvas = new SpyCanvas();
        picture.Draw(canvas);

        Assert.Equal(1, g1.DrawCallCount);
        Assert.Equal(1, g2.DrawCallCount);
    }
}