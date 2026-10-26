using Shapes.Core;

namespace Shapes.Observers;

public class PictureLogger(TextWriter output) : IPictureListener
{
    private readonly TextWriter _output = output ?? throw new ArgumentNullException(nameof(output));

    public void OnPictureChanged(Picture picture)
    {
        ArgumentNullException.ThrowIfNull(picture);

        _output.WriteLine("Picture changed:");

        IReadOnlyList<Shape> shapes = picture.GetShapes();

        if (shapes.Count == 0)
        {
            _output.WriteLine("  Picture is empty");
            return;
        }

        foreach (Shape shape in shapes)
        {
            string paramsString = shape.GetParamsString();
            string line = string.IsNullOrWhiteSpace(paramsString)
                ? $"  {shape.Id} {shape.GetTypeName()} {shape.Color}"
                : $"  {shape.Id} {shape.GetTypeName()} {shape.Color} {paramsString}";

            _output.WriteLine(line);
        }
    }
}