namespace Shapes.Formatting;

public static class NumberFormat
{
    public static double Parse(string s) =>
        double.Parse(s, System.Globalization.CultureInfo.InvariantCulture);
    
    public static string Format(double v) =>
        v.ToString("0.###", System.Globalization.CultureInfo.InvariantCulture);
}