using Shapes.Gfx;

namespace Shapes.Core;

public class Picture : IShapeListener, IDisposable
{
    private readonly List<Shape> _shapes = [];

    private readonly ListenerList<IPictureListener> _listeners;
    
    private bool _disposed;

    public Subscription Subscribe(IPictureListener listener)
    {
        return _listeners.Subscribe(listener);
    }

    public void Dispose()
    {
        if (_disposed) return;
            
        _disposed = true;
        
        foreach (var shape in _shapes) shape.RemoveListener(this);
        
        _shapes.Clear();
        _listeners.Dispose();
    }

    public Picture()
    {
        _listeners = new ListenerList<IPictureListener>(
            listener => listener.OnPictureChanged(this)
        );
    }

    public void AddShape(Shape shape)
    {
        if (GetShape(shape.Id) != null)
        {
            throw new InvalidOperationException(
                $"Shape with id '{shape.Id}' already exists."
            );
        }

        _shapes.Add(shape);
        shape.AddListener(this);

        _listeners.Notify();
    }

    public Shape? GetShape(string id)
    {
        return _shapes.FirstOrDefault(shape => shape.Id == id);
    }

    public void DeleteShape(string id)
    {
        Shape? shape = GetShape(id);

        if (shape == null)
        {
            throw new InvalidOperationException(
                $"Shape with id '{id}' does not exist."
            );
        }

        shape.RemoveListener(this);
        _shapes.Remove(shape);

        _listeners.Notify();
    }

    public void CloneShape(string id, string newId)
    {
        Shape? shape = GetShape(id);

        if (shape == null)
        {
            throw new InvalidOperationException(
                $"Shape with id '{id}' does not exist."
            );
        }

        Shape newShape = shape.Clone(newId);

        AddShape(newShape);
    }

    public IReadOnlyList<Shape> GetShapes()
    {
        return [.. _shapes];
    }

    public void MovePicture(double dx, double dy)
    {
        foreach (Shape shape in _shapes)
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

    public void AddListener(IPictureListener listener)
    {
        _listeners.Add(listener);
    }

    public void RemoveListener(IPictureListener listener)
    {
        _listeners.Remove(listener);
    }

    public void OnShapeChanged(Shape shape)
    {
        _listeners.Notify();
    }
}