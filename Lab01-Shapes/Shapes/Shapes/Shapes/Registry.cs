namespace Shapes.Shapes;

public class Registry
{
    private readonly Dictionary<string, Func<string, IShapeGeometry>> _factories = 
        new(StringComparer.OrdinalIgnoreCase);

    public void Register(string typeName, Func<string, IShapeGeometry> factory)
    {
        if (!_factories.TryAdd(typeName, factory))
            throw new InvalidOperationException($"Shape type '{typeName}' is already registered.");
    }

    public IShapeGeometry Create(string typeName, string rawParams)
    {
        return !_factories.TryGetValue(typeName, out var factory) 
            ? throw new InvalidOperationException($"Unknown shape type '{typeName}'.") 
            : factory(rawParams);
    }
}