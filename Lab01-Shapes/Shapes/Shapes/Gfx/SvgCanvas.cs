using System.Net;
using System.Text;
using Shapes.Formatting;

namespace Shapes.Gfx;

public class SvgCanvas(double width, double height) : ICanvas
{
    private readonly List<string> _elements = [];
    private readonly double _width = width;
    private readonly double _height = height;

    private double _currentX;
    private double _currentY;
    private string _currentColor = "#000000";

    public void SetColor(Color color)
    {
        _currentColor = color.ToString();
    }

    public void MoveTo(double x, double y)
    {
        _currentX = x;
        _currentY = y;
    }

    public void LineTo(double x, double y)
    {
        _elements.Add(
            $"<line x1=\"{NumberFormat.Format(_currentX)}\" " +
            $"y1=\"{NumberFormat.Format(_currentY)}\" " +
            $"x2=\"{NumberFormat.Format(x)}\" " +
            $"y2=\"{NumberFormat.Format(y)}\" " +
            $"stroke=\"{_currentColor}\" />");

        _currentX = x;
        _currentY = y;
    }

    public void DrawEllipse(double cx, double cy, double rx, double ry)
    {
        _elements.Add(
            $"<ellipse cx=\"{NumberFormat.Format(cx)}\" " +
            $"cy=\"{NumberFormat.Format(cy)}\" " +
            $"rx=\"{NumberFormat.Format(rx)}\" " +
            $"ry=\"{NumberFormat.Format(ry)}\" " +
            $"fill=\"{_currentColor}\" />");
    }

    public void DrawText(double left, double top, double fontSize, string text)
    {
        _elements.Add(
            $"<text x=\"{NumberFormat.Format(left)}\" " +
            $"y=\"{NumberFormat.Format(top)}\" " +
            $"font-size=\"{NumberFormat.Format(fontSize)}\" " +
            $"dominant-baseline=\"hanging\" " +
            $"fill=\"{_currentColor}\">" +
            $"{WebUtility.HtmlEncode(text)}</text>");
    }

    public void Save(string path)
    {
        var sb = new StringBuilder();

        sb.AppendLine("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
        sb.AppendLine(
            $"<svg xmlns=\"http://www.w3.org/2000/svg\" " +
            $"width=\"{NumberFormat.Format(_width)}\" " +
            $"height=\"{NumberFormat.Format(_height)}\" " +
            $"viewBox=\"0 0 {NumberFormat.Format(_width)} {NumberFormat.Format(_height)}\">");

        foreach (var element in _elements)
            sb.AppendLine(element);

        sb.AppendLine("</svg>");

        File.WriteAllText(path, sb.ToString());
    }
}
