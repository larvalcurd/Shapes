using Shapes.Commands.Parsers;
using Shapes.Shapes;

namespace Shapes.Commands.Handlers;

public class ChangeColorCommand(Picture picture) : ICommand
{
    private readonly Picture _picture = picture ?? throw new ArgumentNullException(nameof(picture));

    public string Execute(string args)
    {
        var tokens = args.Split([' '], StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length != 2)
        {
            throw new ArgumentException("Command 'ChangeColor' requires exactly 2 arguments: id and color.");
        }

        var id = tokens[0];

        var newColor = ColorParser.Parse(tokens[1]);

        Shape? shape = _picture.GetShape(id) ?? throw new InvalidOperationException($"Shape with id '{id}' does not exist.");

        shape.Color = newColor;
        return $"Changed color of shape '{id}' to {newColor}";
    }
}
