using Shapes.Formatting;
using Shapes.Gfx;

namespace Shapes.Shapes.Geometries;

public class TriangleGeometry : IShapeGeometry
{
    private double _x1, _y1, _x2, _y2, _x3, _y3;

    public TriangleGeometry(
        double x1, double y1,
        double x2, double y2,
        double x3, double y3)
    {
        _x1 = x1;
        _y1 = y1;
        _x2 = x2;
        _y2 = y2;
        _x3 = x3;
        _y3 = y3;
    }

    public string TypeName => "triangle";

    public void Move(double dx, double dy)
    {
        _x1 += dx;
        _y1 += dy;
        _x2 += dx;
        _y2 += dy;
        _x3 += dx;
        _y3 += dy;
    }

    public void Draw(ICanvas canvas)
    {
        canvas.MoveTo(_x1, _y1);
        canvas.LineTo(_x2, _y2);
        canvas.LineTo(_x3, _y3);
        canvas.LineTo(_x1, _y1);
    }

    public string GetParamsString() =>
        $"{NumberFormat.Format(_x1)} {NumberFormat.Format(_y1)} " +
        $"{NumberFormat.Format(_x2)} {NumberFormat.Format(_y2)} " +
        $"{NumberFormat.Format(_x3)} {NumberFormat.Format(_y3)}";

    public IShapeGeometry Clone() =>
        new TriangleGeometry(
            _x1, _y1,
            _x2, _y2,
            _x3, _y3);

    public static TriangleGeometry Parse(string raw)
    {
        var p = raw.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return new TriangleGeometry(
            NumberFormat.Parse(p[0]),
            NumberFormat.Parse(p[1]),
            NumberFormat.Parse(p[2]),
            NumberFormat.Parse(p[3]),
            NumberFormat.Parse(p[4]),
            NumberFormat.Parse(p[5]));
    }
}