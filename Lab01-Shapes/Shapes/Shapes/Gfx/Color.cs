namespace Shapes.Gfx;

public struct Color(byte r, byte g, byte b)
{
    public byte R { get; } = r;
    public byte G { get; } = g;
    public byte B { get; } = b;

    public override string ToString()
        => $"#{R:X2}{G:X2}{B:X2}"; 
}