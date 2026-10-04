using Shapes.Gfx;

namespace Shapes.Shapes;

public class Shape(string id, Color color, IShapeGeometry geometry)
{
    public string Id { get; } = id;
    public Color Color { get; set; } = color;
    private IShapeGeometry _geometry = geometry;

    public void Move(double dx, double dy) => _geometry.Move(dx, dy);

    public void Draw(ICanvas canvas)
    {
        canvas.SetColor(Color);
        _geometry.Draw(canvas);
    }

    public void ReplaceGeometry(IShapeGeometry newGeometry)
    {
        _geometry = newGeometry;
    }

    public string GetTypeName() => _geometry.TypeName;

    public string GetParamsString() => _geometry.GetParamsString();

    public Shape Clone(string newId) => new Shape(newId, Color, _geometry.Clone());
}