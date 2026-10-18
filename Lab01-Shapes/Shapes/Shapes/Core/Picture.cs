using Shapes.Gfx;

namespace Shapes.Core;

public class Picture : IShapeListener
{
    private readonly List<Shape> _shapes = [];

    private readonly List<IPictureListener> _pictureListeners = [];

    public void AddShape(Shape shape)
    {
        if (GetShape(shape.Id) != null)
            throw new InvalidOperationException($"Shape with id '{shape.Id}' already exists.");

        _shapes.Add(shape);
        shape.AddListener(this);
        NotifyListeners();
    }

    public Shape? GetShape(string id)
    {
        return _shapes.FirstOrDefault(shape => shape.Id == id);
    }

    public void DeleteShape(string id)
    {
        var shape = GetShape(id) ?? throw new InvalidOperationException($"Shape with id '{id}' does not exist.");
        
        shape.RemoveListener(this);
        _shapes.Remove(shape);
        NotifyListeners();
    }

    public void CloneShape(string id, string newId)
    {
        var shape = GetShape(id) ?? throw new InvalidOperationException($"Shape with id '{id}' does not exist.");
        AddShape(shape.Clone(newId));
    }

    public IReadOnlyList<Shape> GetShapes() => [.. _shapes];

    public void MovePicture(double dx, double dy)
    {
        foreach (var shape in _shapes)
            shape.Move(dx, dy);
    }

    public void DrawPicture(ICanvas canvas)
    {
        foreach (var shape in _shapes)
            shape.Draw(canvas);
    }

    public void AddListener(IPictureListener listener)
    {
        if (!_pictureListeners.Contains(listener))
        {
            _pictureListeners.Add(listener);
        }
    }

    public void RemoveListener(IPictureListener listener)
    {
        _pictureListeners.Remove(listener);
    }

    private void NotifyListeners()
    {
        foreach (var listener in _pictureListeners)
        {
            listener.OnPictureChanged(this);
        }
    }
    
    public void OnShapeChanged(Shape shape)
    {
        NotifyListeners();
    }
}
