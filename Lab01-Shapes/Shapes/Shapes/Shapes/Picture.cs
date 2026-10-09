using Shapes.Gfx;

namespace Shapes.Shapes;

public class Picture
{
    private readonly List<Shape> _shapes = [];

    public void AddShape(Shape shape)
    {
        if (GetShape(shape.Id) != null)
            throw new InvalidOperationException($"Shape with id '{shape.Id}' already exists.");

        _shapes.Add(shape);
    }

    public Shape? GetShape(string id)
    {
        return _shapes.FirstOrDefault(shape => shape.Id == id);
    }

    public void DeleteShape(string id)
    {
        var shape = GetShape(id) ?? throw new InvalidOperationException($"Shape with id '{id}' does not exist.");
        _shapes.Remove(shape);
    }

    public void CloneShape(string id, string newId)
    {
        if (GetShape(newId) != null)
            throw new InvalidOperationException($"Shape with id '{newId}' already exists.");

        var shape = GetShape(id) ?? throw new InvalidOperationException($"Shape with id '{id}' does not exist.");
        _shapes.Add(shape.Clone(newId));
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
}
