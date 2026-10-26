using Shapes.Core;
using Shapes.Gfx;
using Shapes.Tests.Mocks;

namespace Shapes.Tests;

public class PictureObserverTests
{
    [Fact]
    public void AddShape_NotifiesExternalListeners()
    {
        Picture picture = new();
        FakePictureListener listener = new();
        picture.AddListener(listener);

        Shape shape = new("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape);

        Assert.Equal(1, listener.CallCount);
        Assert.Same(picture, listener.LastNotifiedPicture);
    }

    [Fact]
    public void MoveShape_DirectReference_NotifiesPictureListeners()
    {
        Picture picture = new();
        Shape shapeToAdd = new("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shapeToAdd);

        FakePictureListener listener = new();
        picture.AddListener(listener);

        Shape shape = picture.GetShape("sh1")!;
        shape.Move(10, 0);

        Assert.Equal(1, listener.CallCount);
        Assert.Same(picture, listener.LastNotifiedPicture);
    }

    [Fact]
    public void AddListener_Twice_DoesNotDuplicateNotifications()
    {
        Picture picture = new();
        FakePictureListener listener = new();

        picture.AddListener(listener);
        picture.AddListener(listener);

        Shape shape = new("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape);

        Assert.Equal(1, listener.CallCount);
    }

    [Fact]
    public void FailedOperation_DoesNotNotifyListeners()
    {
        Picture picture = new();
        Shape shape = new("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape);

        FakePictureListener listener = new();
        picture.AddListener(listener);

        Shape duplicateShape = new("sh2", new Color(0, 255, 0), new FakeGeometry());
        Assert.Throws<InvalidOperationException>(() => picture.AddShape(duplicateShape));

        Assert.Equal(0, listener.CallCount);
    }

    [Fact]
    public void DeleteShape_NotifiesExternalListeners()
    {
        Picture picture = new();
        Shape shape = new("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape);

        FakePictureListener listener = new();
        picture.AddListener(listener);

        picture.DeleteShape("sh1");

        Assert.Equal(1, listener.CallCount);
        Assert.Same(picture, listener.LastNotifiedPicture);
    }

    [Fact]
    public void DeleteShape_UnsubscribesFromShape_DirectMoveDoesNotNotify()
    {
        Picture picture = new();
        Shape shape = new("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape);

        picture.DeleteShape("sh1");

        FakePictureListener listener = new();
        picture.AddListener(listener);

        shape.Move(10, 0);

        Assert.Equal(0, listener.CallCount);
    }

    [Fact]
    public void MultipleListeners_AllReceiveNotifications()
    {
        Picture picture = new();
        FakePictureListener listener1 = new();
        FakePictureListener listener2 = new();

        picture.AddListener(listener1);
        picture.AddListener(listener2);

        Shape shape = new("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape);

        Assert.Equal(1, listener1.CallCount);
        Assert.Equal(1, listener2.CallCount);
        Assert.Same(picture, listener1.LastNotifiedPicture);
        Assert.Same(picture, listener2.LastNotifiedPicture);
    }

    [Fact]
    public void RemoveListener_StopsReceivingNotifications()
    {
        Picture picture = new();
        FakePictureListener listener = new();
        picture.AddListener(listener);

        Shape shape1 = new("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape1);
        Assert.Equal(1, listener.CallCount);

        picture.RemoveListener(listener);

        Shape shape2 = new("sh2", new Color(0, 255, 0), new FakeGeometry());
        picture.AddShape(shape2);
        Assert.Equal(1, listener.CallCount);
    }

    [Fact]
    public void Listener_RemovesItself_DuringNotification_DoesNotThrow()
    {
        Picture picture = new();
        SelfRemovingPictureListener selfRemoving = new();
        picture.AddListener(selfRemoving);

        Shape shape = new("sh1", new Color(255, 0, 0), new FakeGeometry());

        Exception? exception = Record.Exception(() => picture.AddShape(shape));

        Assert.Null(exception);
    }

    [Fact]
    public void Picture_Disposed_SubscriptionDisposeDoesNotThrow()
    {
        Picture picture = new();
        FakePictureListener logger = new();
        Subscription subscription = picture.Subscribe(logger);

        picture.Dispose();

        Exception? exception = Record.Exception(() => subscription.Dispose());
        Assert.Null(exception);
    }

    [Fact]
    public void Subscription_Active_ListenerNotified()
    {
        Picture picture = new();
        FakePictureListener listener = new();

        using Subscription subscription = picture.Subscribe(listener);

        Shape shape = new("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape);

        Assert.Equal(1, listener.CallCount);
    }

    [Fact]
    public void Subscription_Disposed_ListenerNotNotified()
    {
        Picture picture = new();
        FakePictureListener listener = new();

        Subscription subscription = picture.Subscribe(listener);

        Shape shape1 = new("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape1);

        Assert.Equal(1, listener.CallCount);

        subscription.Dispose();

        Shape shape2 = new("sh2", new Color(0, 255, 0), new FakeGeometry());
        picture.AddShape(shape2);

        Assert.Equal(1, listener.CallCount);
    }

    [Fact]
    public void Subscription_DisposedTwice_DoesNotThrow()
    {
        Picture picture = new();
        FakePictureListener listener = new();

        Subscription subscription = picture.Subscribe(listener);

        Shape shape = new("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape);

        Assert.Equal(1, listener.CallCount);

        subscription.Dispose();


        Exception? exception = Record.Exception(() => subscription.Dispose());

        Assert.Null(exception);
    }


    [Fact]
    public void Subscription_Take_FirstDisposeNoOp_SecondOwns()
    {
        Picture picture = new();
        FakePictureListener listener = new();

        Subscription subscription = picture.Subscribe(listener);
        Subscription newSubscription = subscription.Take();

        subscription.Dispose();

        Shape shape1 = new("sh1", new Color(255, 0, 0), new FakeGeometry());
        picture.AddShape(shape1);

        Assert.Equal(1, listener.CallCount);

        newSubscription.Dispose();

        Shape shape2 = new("sh2", new Color(0, 255, 0), new FakeGeometry());
        picture.AddShape(shape2);

        Assert.Equal(1, listener.CallCount);
    }

    [Fact]
    public void Subscription_DoesNotKeepPictureAlive()
    {
        FakePictureListener listener = new();

        (WeakReference<Picture> pictureReference, Subscription subscription) =
            CreateDisposedPictureWithSubscription(listener);

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        Assert.False(pictureReference.TryGetTarget(out _));

        subscription.Dispose();
    }

    private static (
        WeakReference<Picture> PictureReference,
        Subscription Subscription)
        CreateDisposedPictureWithSubscription(FakePictureListener listener)
    {
        Picture picture = new();
        Subscription subscription = picture.Subscribe(listener);
        WeakReference<Picture> pictureReference = new(picture);

        picture.Dispose();

        return (pictureReference, subscription);
    }


    [Fact]
    public void Debug_SubscriptionAndPictureReferences()
    {
        Picture picture = new();
        FakePictureListener listener = new();

        Subscription subscription = picture.Subscribe(listener);

        // BREAKPOINT 1: все объекты ещё живы.
        // Посмотри на picture, subscription и listener.

        WeakReference<Picture> pictureReference = new(picture);

        picture.Dispose();

        // BREAKPOINT 2: picture.Dispose() уже выполнен.
        // Посмотри на pictureReference и subscription.
        // Внутри ListenerList поле _notifyAction должно быть null.

        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        // BREAKPOINT 3: проверь, собран ли Picture.
        bool pictureAlive = pictureReference.TryGetTarget(out _);

        Assert.False(pictureAlive);

        // Subscription всё ещё существует.
        // Его Dispose() должен завершиться без исключения.
        subscription.Dispose();
    }
}
