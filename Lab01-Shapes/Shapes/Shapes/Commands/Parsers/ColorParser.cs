using Shapes.Gfx;

namespace Shapes.Commands.Parsers;

public static class ColorParser
{
    public static Color Parse(string value)
    {
        if (value is not ['#', _, _, _, _, _, _])
            throw new FormatException($"Invalid color format: '{value}'");

        try
        {
            var r = Convert.ToByte(value.Substring(1, 2), 16);
            var g = Convert.ToByte(value.Substring(3, 2), 16);
            var b = Convert.ToByte(value.Substring(5, 2), 16);

            return new Color(r, g, b);
        }
        catch (FormatException)
        {
            throw new FormatException($"Invalid color format: '{value}'");
        }
    }
}