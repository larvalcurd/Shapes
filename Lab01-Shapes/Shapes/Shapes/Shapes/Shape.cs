using Shapes.Gfx;

namespace Shapes.Shapes;

public class Shape(string id, Color color, IShapeGeometry geometry)
{
    public string Id { get; } = id;
    public Color Color { get; set; } = color;
    public IShapeGeometry Geometry { get; set; } = geometry;

    public void Move(double dx, double dy) => Geometry.Move(dx, dy);
    public void Draw(ICanvas canvas) => Geometry.Draw(canvas, Color);
    
    public Shape Clone(string newId) => new Shape(newId, Color, Geometry.Clone());
}