using Shapes.Core;
using Shapes.Gfx;

namespace Shapes.Commands.Handlers;

public class DrawPictureCommand(Picture picture, ICanvas canvas) : ICommand
{
    private readonly ICanvas _canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
    private readonly Picture _picture = picture ?? throw new ArgumentNullException(nameof(picture));

    public string Execute(string args)
    {
        string[] tokens = args.Split([' '], StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length > 0)
        {
            throw new ArgumentException("Command 'DrawPicture' does not accept any arguments.");
        }


        _picture.DrawPicture(_canvas);
        int count = _picture.GetShapes().Count;
        string shapeWord = count == 1 ? "shape" : "shapes";
        return $"Drew picture with {count} shapes";
    }
}
