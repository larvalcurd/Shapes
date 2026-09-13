using Shapes.Shapes.Geometries;

namespace Shapes.Shapes.ShapeFactory;

public static class DefaultFactory
{
    public static ShapeGeometryFactory CreateDefault()
    {
        var factory = new ShapeGeometryFactory();
        factory.Register("circle", CircleGeometry.Parse);
        factory.Register("rectangle", RectangleGeometry.Parse);
        factory.Register("triangle", TriangleGeometry.Parse);
        factory.Register("line", LineGeometry.Parse);
        factory.Register("text", TextGeometry.Parse);
        return factory;
    }
}