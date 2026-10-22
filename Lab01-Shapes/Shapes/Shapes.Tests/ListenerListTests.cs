using Shapes.Gfx;
using Shapes.Core;
using Shapes.Tests.Mocks;

namespace Shapes.Tests;

public class ActionableListener : IPictureListener
{
    public int CallCount { get; private set; } = 0;
    public Action<Picture>? OnNotified { get; set; }

    public void OnPictureChanged(Picture picture)
    {
        CallCount++;
        OnNotified?.Invoke(picture);
    }
}

public class ListenerListTests
{
    [Fact]
    public void Listener_RemovesAnother_SecondDoesNotGetNotified()
    {
        var picture = new Picture();
        var a = new ActionableListener();
        var b = new ActionableListener();

        picture.AddListener(a);
        picture.AddListener(b);

        a.OnNotified = p => p.RemoveListener(b);

        var shape = new Shape("sh1", new Color(255, 0, 0), new FakeGeometry());

        picture.AddShape(shape);

        Assert.Equal(1, a.CallCount);
        Assert.Equal(0, b.CallCount);
    }

    [Fact]
    public void Listener_RemovesOne_OthersStillNotified()
    {
        var picture = new Picture();
        var a = new ActionableListener();
        var b = new ActionableListener();
        var c = new ActionableListener();

        picture.AddListener(a);
        picture.AddListener(b);
        picture.AddListener(c);

        a.OnNotified = p => p.RemoveListener(b);

        var shape = new Shape("sh1", new Color(255, 0, 0), new FakeGeometry());

        picture.AddShape(shape);

        Assert.Equal(1, a.CallCount);
        Assert.Equal(0, b.CallCount);
        Assert.Equal(1, c.CallCount);
    }

    [Fact]
    public void Listener_AddsNew_NewDoesNotGetCurrent()
    {
        var picture = new Picture();

        var a = new ActionableListener();
        var b = new ActionableListener();

        picture.AddListener(a);

        bool added = false;

        a.OnNotified = p =>
        {
            if (added) return;
            p.AddListener(b);
            added = true;
        };

        var shape1 = new Shape("sh1", new Color(255, 0, 0), new FakeGeometry());

        picture.AddShape(shape1);


        Assert.Equal(1, a.CallCount);
        Assert.Equal(0, b.CallCount);

        var shape2 = new Shape("sh2", new Color(0, 255, 0), new FakeGeometry());

        picture.AddShape(shape2);

        Assert.Equal(2, a.CallCount);
        Assert.Equal(1, b.CallCount);
    }

    [Fact]
    public void Listener_RemovedThenReadded_DoesNotGetCurrent()
    {
        var picture = new Picture();

        var a = new ActionableListener();
        var b = new ActionableListener();

        picture.AddListener(a);
        picture.AddListener(b);

        a.OnNotified = p =>
        {
            p.RemoveListener(b);
            p.AddListener(b);
        };

        var shape1 = new Shape("sh1", new Color(255, 0, 0), new FakeGeometry());

        picture.AddShape(shape1);

        Assert.Equal(1, a.CallCount);
        Assert.Equal(0, b.CallCount);

        a.OnNotified = null;

        var shape2 = new Shape("sh2", new Color(0, 255, 0), new FakeGeometry());

        picture.AddShape(shape2);

        Assert.Equal(2, a.CallCount);
        Assert.Equal(1, b.CallCount);
    }

    [Fact]
    public void SelfRemoving_NoDoubleCall()
    {
        var picture = new Picture();

        var a = new ActionableListener();

        picture.AddListener(a);

        a.OnNotified = p => p.RemoveListener(a);

        var shape = new Shape("sh1", new Color(255, 0, 0), new FakeGeometry());

        picture.AddShape(shape);

        Assert.Equal(1, a.CallCount);
    }
}
