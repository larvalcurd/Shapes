namespace Shapes.Shapes;

public interface IShapeGeometry
{
    string TypeName { get; }
    void Move(double dx, double dy);
    void Draw(Gfx.ICanvas canvas, Gfx.Color color);
    string GetParamsString();
}