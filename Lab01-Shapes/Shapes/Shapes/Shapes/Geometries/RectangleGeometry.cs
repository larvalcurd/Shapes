using Shapes.Formatting;
using Shapes.Gfx;

namespace Shapes.Shapes.Geometries;

public class RectangleGeometry : IShapeGeometry
{
    private double _left, _top, _width, _height;

    public RectangleGeometry(double left, double top, double width, double height)
    {
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(width, 0);
        ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(height, 0);
        
        _left = left;
        _top = top;
        _width = width;
        _height = height;
    }
    
    public string TypeName => "rectangle";

    public void Move(double dx, double dy)
    {
        _left += dx; 
        _top += dy;
    }

    public void Draw(ICanvas canvas, Color color)
    {
        canvas.SetColor(color);
        canvas.MoveTo(_left, _top);
        canvas.LineTo(_left + _width, _top);
        canvas.LineTo(_left + _width, _top + _height);
        canvas.LineTo(_left, _top + _height);
        canvas.LineTo(_left, _top);
    }

    public string GetParamsString() => $"{NumberFormat.Format(_left)} {NumberFormat.Format(_top)} {NumberFormat.Format(_width)} {NumberFormat.Format(_height)}";


    public static RectangleGeometry Parse(string raw)
    {
        var p = raw.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        return new RectangleGeometry(
            NumberFormat.Parse(p[0]),
            NumberFormat.Parse(p[1]),
            NumberFormat.Parse(p[2]),
            NumberFormat.Parse(p[3])
        );
    }
}