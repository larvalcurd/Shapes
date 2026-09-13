namespace Shapes.Shapes.ShapeFactory;

public interface IShapeGeometryFactory
{
    IShapeGeometry Create(string typeName, string paramsString);
    void Register(string typeName, Func<string, IShapeGeometry> creator);
}