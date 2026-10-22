using Shapes.Core;
using Shapes.Gfx;

namespace Shapes.Commands.Handlers;

public class DrawShapeCommand(Picture picture, ICanvas canvas) : ICommand
{
    private readonly ICanvas _canvas = canvas ?? throw new ArgumentNullException(nameof(canvas));
    private readonly Picture _picture = picture ?? throw new ArgumentNullException(nameof(picture));

    public string Execute(string args)
    {
        string[] tokens = args.Split([' '], StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length != 1)
        {
            throw new ArgumentException("Command 'DrawShape' requires exactly 1 argument: id.");
        }

        string id = tokens[0];

        Shape? shape = _picture.GetShape(id)
                       ?? throw new InvalidOperationException($"Shape with id '{id}' does not exist.");

        shape.Draw(_canvas);
        return $"Drew shape {id}";
    }
}
