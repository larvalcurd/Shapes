using Shapes.Commands.Parsers;
using Shapes.Shapes;

namespace Shapes.Commands.Handlers;

public class AddShapeCommand(Picture picture, Registry registry) : ICommand
{
    private readonly Picture _picture = picture ?? throw new ArgumentNullException(nameof(picture));
    private readonly Registry _registry = registry ?? throw new ArgumentNullException(nameof(registry));

    public string Execute(string args)
    {
        var tokens = args.Split([' '], 4, StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length < 4)
        {
            throw new ArgumentException("Command 'AddShape' requires at least 4 arguments: id, color, type, and parameters.");
        }

        var id = tokens[0];
        var colorRaw = tokens[1];
        var type = tokens[2];
        var paramsString = tokens[3];

        var color = ColorParser.Parse(colorRaw);

        var geometry = _registry.Create(type, paramsString);

        var shape = new Shape(id, color, geometry);

        _picture.AddShape(shape);
        return $"Added {type} '{id}' with parameters: {paramsString}";
    }
}
