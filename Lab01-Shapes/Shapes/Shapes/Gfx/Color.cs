namespace Shapes.Gfx;

// record struct дает сравнение по значениям бесплатно
public readonly record struct Color(byte R, byte G, byte B)
{
    public override string ToString() => $"#{R:X2}{G:X2}{B:X2}";
}