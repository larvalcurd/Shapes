using Shapes.Gfx;
using Shapes.Shapes;

namespace Shapes.Commands.Handlers;

public class DrawPictureCommand(Picture picture, ICanvas canvas) : ICommand
{
    private readonly Picture _picture = picture ?? throw new ArgumentNullException(nameof(picture));
    private readonly ICanvas _canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));

    public string Execute(string args)
    {
        var tokens = args.Split([' '], StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length > 0)
        {
            throw new ArgumentException("Command 'DrawPicture' does not accept any arguments.");
        }


        
        _picture.DrawPicture(_canvas);
        var count = _picture.GetShapes().Count; 
        var shapeWord = count == 1 ? "shape" : "shapes";
        return $"Drew picture with {count} shapes";
    }
}
