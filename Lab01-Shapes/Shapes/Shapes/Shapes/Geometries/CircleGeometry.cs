using Shapes.Formatting;
using Shapes.Gfx;

namespace Shapes.Shapes.Geometries;

public class CircleGeometry : IShapeGeometry
{
    private double _x, _y;
    private readonly double _r;

    public CircleGeometry(double x, double y, double r)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(r, 0);
        _x = x; 
        _y = y; 
        _r = r;
    }

    public string TypeName => "circle";

    public void Move(double dx, double dy)
    {
        _x += dx; _y += dy;
    }

    public void Draw(ICanvas canvas, Color color)
    {
        canvas.SetColor(color);
        canvas.DrawEllipse(_x, _y, _r, _r);
    }

    public string GetParamsString() =>
        $"{NumberFormat.Format(_x)} {NumberFormat.Format(_y)} {NumberFormat.Format(_r)}";
    
    public IShapeGeometry Clone() => (IShapeGeometry)MemberwiseClone();

    public static CircleGeometry Parse(string raw)
    {
        var p = raw.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return new CircleGeometry(
            NumberFormat.Parse(p[0]),
            NumberFormat.Parse(p[1]),
            NumberFormat.Parse(p[2])
        );
    }
}