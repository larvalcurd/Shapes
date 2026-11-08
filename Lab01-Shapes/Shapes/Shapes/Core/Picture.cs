using Shapes.Core.Events;
using Shapes.Core.Listeners;
using Shapes.Gfx;

namespace Shapes.Core;

public class Picture : IDisposable
{
    private readonly List<Shape> _shapes = [];
    private readonly Dictionary<string, ShapeSubscriptions> _shapeSubscriptions = [];

    private readonly Signal<ShapeAddedArgs> _added = new();
    private readonly Signal<ShapeRemovedArgs> _removed = new();
    private readonly Signal<ShapeMovedArgs> _moved = new();
    private readonly Signal<ShapeColorChangedArgs> _colorChanged = new();
    private readonly Signal<ShapeGeometryChangedArgs> _geometryChanged = new();

    private bool _disposed;

    public Subscription SubscribeToShapeAdded(Action<Shape> handler)
    {
        ArgumentNullException.ThrowIfNull(handler);
        return _added.Subscribe(args => handler(args.Shape));
    }

    public Subscription SubscribeToShapeRemoved(Action<Shape> handler)
    {
        ArgumentNullException.ThrowIfNull(handler);
        return _removed.Subscribe(args => handler(args.Shape));
    }

    public Subscription SubscribeToShapeMoved(Action<Shape, double, double> handler)
    {
        ArgumentNullException.ThrowIfNull(handler);
        return _moved.Subscribe(args => handler(args.Shape, args.Dx, args.Dy));
    }

    public Subscription SubscribeToShapeColorChanged(Action<Shape, Color> handler)
    {
        ArgumentNullException.ThrowIfNull(handler);
        return _colorChanged.Subscribe(args => handler(args.Shape, args.OldColor));
    }

    public Subscription SubscribeToShapeGeometryChanged(Action<Shape, IShapeGeometry> handler)
    {
        ArgumentNullException.ThrowIfNull(handler);
        return _geometryChanged.Subscribe(args => handler(args.Shape, args.OldGeometry));
    }

    public void AddShape(Shape shape)
    {
        ArgumentNullException.ThrowIfNull(shape);

        if (GetShape(shape.Id) != null)
        {
            throw new InvalidOperationException(
                $"Shape with id '{shape.Id}' already exists."
            );
        }

        var subs = new ShapeSubscriptions(
            Moved: shape.OnMoved(a => _moved.Notify(a)),
            Color: shape.OnColorChanged(a => _colorChanged.Notify(a)),
            Geometry: shape.OnGeometryChanged(a => _geometryChanged.Notify(a))
        );

        _shapes.Add(shape);
        _shapeSubscriptions[shape.Id] = subs;

        _added.Notify(new ShapeAddedArgs(shape));
    }

    public Shape? GetShape(string id) => _shapes.FirstOrDefault(shape => shape.Id == id);

    public void DeleteShape(string id)
    {
        Shape? shape = GetShape(id) ?? throw new InvalidOperationException(
                $"Shape with id '{id}' does not exist."
            );

        if (_shapeSubscriptions.Remove(id, out ShapeSubscriptions? subscriptions))
        {
            subscriptions.Moved.Dispose();
            subscriptions.Color.Dispose();
            subscriptions.Geometry.Dispose();
        }

        _shapes.Remove(shape);

        _removed.Notify(new ShapeRemovedArgs(shape));
    }

    public void CloneShape(string id, string newId)
    {
        Shape? shape = GetShape(id) ?? throw new InvalidOperationException(
                $"Shape with id '{id}' does not exist."
            );

        Shape newShape = shape.Clone(newId);

        AddShape(newShape);
    }

    public IReadOnlyList<Shape> GetShapes() => [.. _shapes];

    public void MovePicture(double dx, double dy)
    {
        foreach (Shape shape in _shapes.ToArray())
        {
            shape.Move(dx, dy);
        }
    }

    public void DrawPicture(ICanvas canvas)
    {
        foreach (Shape shape in _shapes)
        {
            shape.Draw(canvas);
        }
    }

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;

        foreach (ShapeSubscriptions subscriptions in _shapeSubscriptions.Values)
        {
            subscriptions.Moved.Dispose();
            subscriptions.Color.Dispose();
            subscriptions.Geometry.Dispose();
        }

        _shapeSubscriptions.Clear();

        _shapes.Clear();

        _added.Dispose();
        _removed.Dispose();
        _moved.Dispose();
        _colorChanged.Dispose();
        _geometryChanged.Dispose();
    }

    private sealed record ShapeSubscriptions(
        Subscription Moved,
        Subscription Color,
        Subscription Geometry
    );
}
