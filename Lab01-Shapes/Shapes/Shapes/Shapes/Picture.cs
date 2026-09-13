namespace Shapes.Shapes;

public class Picture
{
    private readonly List<Shape> _order = [];
    private readonly Dictionary<string, Shape> _byId = [];

    public void AddShape(Shape shape)
    {
        if (!_byId.TryAdd(shape.Id, shape))
            throw new InvalidOperationException($"Shape with id '{shape.Id}' already exists");
        
        _order.Add(shape);
    }
    
    public void DeleteShape(string id)
    {
        if (!_byId.Remove(id, out var shape))
            throw new InvalidOperationException($"Shape with id '{id}' not found");

        _order.Remove(shape);
    }
    
    public Shape GetShape(string id)
    {
        return _byId.TryGetValue(id, out var shape) 
            ? shape 
            : throw new InvalidOperationException($"Shape with id '{id}' not found");
    }
    
    public IReadOnlyList<Shape> GetShapesInOrder() => _order;

    public void Move(double dx, double dy)
    {
        foreach (var shape in _order)
        {
            shape.Move(dx, dy);
        }
    }

    public void Draw(Gfx.ICanvas canvas)
    {
        foreach (var shape in _order) 
            shape.Draw(canvas);
    }
}