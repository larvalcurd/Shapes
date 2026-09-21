using Shapes.Formatting;
using Shapes.Gfx;

namespace Shapes.Shapes.Geometries;

public class LineGeometry : IShapeGeometry
{
    private double _x1, _y1, _x2, _y2;

    public LineGeometry(double x1, double y1, double x2, double y2)
    {
        if (Math.Abs(x1 - x2) < 1e-10 && Math.Abs(y1 - y2) <= 1e-10)
            throw new ArgumentException("The points coincide and do not form a valid line");
        
        _x1 = x1;
        _y1 = y1;
        _x2 = x2;
        _y2 = y2;
    }

    public string TypeName => "line";
    public void Move(double dx, double dy)
    {
        _x1 += dx;
        _y1 += dy;
        _x2 += dx;
        _y2 += dy;
    }

    public void Draw(ICanvas canvas, Color color)
    {
        canvas.SetColor(color);
        canvas.MoveTo(_x1, _y1);
        canvas.LineTo(_x2, _y2);
    }

    public string GetParamsString() =>
        $"{NumberFormat.Format(_x1)} {NumberFormat.Format(_y1)} " +
        $"{NumberFormat.Format(_x2)} {NumberFormat.Format(_y2)}";
    
    public IShapeGeometry Clone() => (IShapeGeometry)MemberwiseClone();
    
    public static LineGeometry Parse(string raw)
    {
        var p = raw.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return new LineGeometry(
            NumberFormat.Parse(p[0]), NumberFormat.Parse(p[1]),
            NumberFormat.Parse(p[2]), NumberFormat.Parse(p[3]));
    }
}