using Shapes.Gfx;

namespace Shapes.Shapes;

public class Shape(string id, Color color, IShapeGeometry geometry)
{
    public string Id { get; } = id;
    public Color Color { get; set; } = color;
    // todo: провести аналогию с ptr в c++, разобраться, какой тип владения геметрией
    // это управляемая ссылка, этим занимается GC, аналог в с++ - это unique_ptr, который владеет объектом и уничтожает его при удалении
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
    // TODO: паттерн "прототип"
    public Shape Clone(string newId) => new Shape(newId, Color, _geometry.Clone());
}