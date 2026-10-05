using Shapes.Formatting;
using Shapes.Gfx;

namespace Shapes.Shapes.Geometries;

public class TextGeometry : IShapeGeometry
{
    private double _left, _top;
    private readonly double _fontSize;
    private readonly string _text;

    public TextGeometry(double left, double top, double fontSize, string text)
    {
        // По спецификации размер шрифта не должен быть отрицательным
        ArgumentOutOfRangeException.ThrowIfNegative(fontSize);

        _left = left;
        _top = top;
        _fontSize = fontSize;
        _text = text;
    }

    public string TypeName => "text";

    public void Move(double dx, double dy)
    {
        _left += dx;
        _top += dy;
    }

    public void Draw(ICanvas canvas)
    {
        canvas.DrawText(_left, _top, _fontSize, _text);
    }

    public string GetParamsString() =>
        $"{NumberFormat.Format(_left)} {NumberFormat.Format(_top)} {NumberFormat.Format(_fontSize)} {_text}";

    public IShapeGeometry Clone() => new TextGeometry(_left, _top, _fontSize, _text);

    public static TextGeometry Parse(string raw)
    {
        var p = raw.Split(' ', 4, StringSplitOptions.RemoveEmptyEntries);

        // Защита от недостаточного количества аргументов (нужно минимум left, top, fontSize)
        if (p.Length < 3)
            throw new ArgumentException("Invalid number of arguments for TextGeometry. Expected at least 3 parameters.");

        var left = NumberFormat.Parse(p[0]);
        var top = NumberFormat.Parse(p[1]);
        var fontSize = NumberFormat.Parse(p[2]);
        var text = p.Length > 3 ? p[3] : "";

        return new TextGeometry(left, top, fontSize, text);
    }
}