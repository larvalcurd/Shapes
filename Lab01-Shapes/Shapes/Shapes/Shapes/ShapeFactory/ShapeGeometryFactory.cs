namespace Shapes.Shapes.ShapeFactory;

public class ShapeGeometryFactory : IShapeGeometryFactory
{
    private readonly Dictionary<string, Func<string, IShapeGeometry>> _creators = new();    
    
    public void Register(string typeName, Func<string, IShapeGeometry> creator) =>  
        _creators[typeName] = creator;
    
    public IShapeGeometry Create(string typeName, string paramsString)
    {
        return !_creators.TryGetValue(typeName, out var creator) 
            ? throw new InvalidOperationException($"Unknown shape type '{typeName}'") 
            : creator(paramsString);
    }
}