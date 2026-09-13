namespace Shapes.Gfx;

public struct Color {
    public byte R, G, B;

    public static Color Parse(string hex)
    {
        if (hex.Length != 7 || hex[0] != '#')
            throw new FormatException($"Invalid color format: '{hex}'");
        
        var r = Convert.ToByte(hex.Substring(1, 2), 16);
        var g = Convert.ToByte(hex.Substring(3, 2), 16);
        var b = Convert.ToByte(hex.Substring(5, 2), 16);
        return new Color { R = r, G = g, B = b };
    }
    public override string ToString() => $"#{R:X2}{G:X2}{B:X2}";
}