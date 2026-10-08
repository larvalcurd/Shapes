namespace Shapes.Commands;

public class CommandDependencies(
    Shapes.Picture picture,
    Gfx.ICanvas canvas,
    Shapes.ShapeFactory.IShapeGeometryFactory factory)
{
    public Shapes.Picture Picture { get; } = picture;
    public Gfx.ICanvas Canvas { get; } = canvas;
    public Shapes.ShapeFactory.IShapeGeometryFactory Factory { get; } = factory;
}