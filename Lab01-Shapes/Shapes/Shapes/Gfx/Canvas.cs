namespace Shapes.Gfx;

public interface ICanvas
{
    public void SetColor(Color color);
    public void MoveTo(double x, double y);
    public void LineTo(double x, double y);
    public void DrawEllipse(double cx, double cy, double rx, double ry);
    public void DrawText(double left, double top, double fontSize, string text);
}