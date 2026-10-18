using Shapes.Gfx;
using Shapes.Core;
using Shapes.Tests.Mocks;

namespace Shapes.Tests;

public class PictureObserverTests
{
    [Fact]
    public void AddShape_NotifiesExternalListeners()
    {
        var picture = new Picture();
        var listener = new FakePictureListener();
        picture.AddListener(listener);

        var shape = new Shape("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape);

        Assert.Equal(1, listener.CallCount);
        Assert.Same(picture, listener.LastNotifiedPicture);
    }

    [Fact]
    public void MoveShape_DirectReference_NotifiesPictureListeners()
    {
        var picture = new Picture();
        var shapeToAdd = new Shape("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shapeToAdd);

        var listener = new FakePictureListener();
        picture.AddListener(listener);

        var shape = picture.GetShape("sh1")!;
        shape.Move(10, 0);

        Assert.Equal(1, listener.CallCount);
        Assert.Same(picture, listener.LastNotifiedPicture);
    }

    [Fact]
    public void AddListener_Twice_DoesNotDuplicateNotifications()
    {
        var picture = new Picture();
        var listener = new FakePictureListener();

        picture.AddListener(listener);
        picture.AddListener(listener);

        var shape = new Shape("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape);

        Assert.Equal(1, listener.CallCount);
    }

    [Fact]
    public void FailedOperation_DoesNotNotifyListeners()
    {
        var picture = new Picture();
        var shape = new Shape("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape);

        var listener = new FakePictureListener();
        picture.AddListener(listener);

        var duplicateShape = new Shape("sh1", new Color(0, 255, 0), new FakeGeometry());
        Assert.Throws<InvalidOperationException>(() => picture.AddShape(duplicateShape));

        Assert.Equal(0, listener.CallCount);
    }

    [Fact]
    public void DeleteShape_NotifiesExternalListeners()
    {
        var picture = new Picture();
        var shape = new Shape("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape);

        var listener = new FakePictureListener();
        picture.AddListener(listener);

        picture.DeleteShape("sh1");

        Assert.Equal(1, listener.CallCount);
        Assert.Same(picture, listener.LastNotifiedPicture);
    }

    [Fact]
    public void DeleteShape_UnsubscribesFromShape_DirectMoveDoesNotNotify()
    {
        var picture = new Picture();
        var shape = new Shape("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape);

        picture.DeleteShape("sh1");

        var listener = new FakePictureListener();
        picture.AddListener(listener);

        shape.Move(10, 0);

        Assert.Equal(0, listener.CallCount);
    }

    [Fact]
    public void MultipleListeners_AllReceiveNotifications()
    {
        var picture = new Picture();
        var listener1 = new FakePictureListener();
        var listener2 = new FakePictureListener();

        picture.AddListener(listener1);
        picture.AddListener(listener2);

        var shape = new Shape("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape);

        Assert.Equal(1, listener1.CallCount);
        Assert.Equal(1, listener2.CallCount);
        Assert.Same(picture, listener1.LastNotifiedPicture);
        Assert.Same(picture, listener2.LastNotifiedPicture);
    }

    [Fact]
    public void RemoveListener_StopsReceivingNotifications()
    {
        var picture = new Picture();
        var listener = new FakePictureListener();
        picture.AddListener(listener);

        var shape1 = new Shape("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape1);
        Assert.Equal(1, listener.CallCount);

        picture.RemoveListener(listener);

        var shape2 = new Shape("sh2", new Color(0, 255, 0), new FakeGeometry());
        picture.AddShape(shape2);
        Assert.Equal(1, listener.CallCount);
    }
}
