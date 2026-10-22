using Shapes.Commands.Parsers;
using Shapes.Core;
using Shapes.Gfx;

namespace Shapes.Commands.Handlers;

public class AddShapeCommand(Picture picture, Registry registry) : ICommand
{
    private readonly Picture _picture = picture ?? throw new ArgumentNullException(nameof(picture));
    private readonly Registry _registry = registry ?? throw new ArgumentNullException(nameof(registry));

    public string Execute(string args)
    {
        string[] tokens = args.Split([' '], 4, StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length < 4)
        {
            throw new ArgumentException(
                "Command 'AddShape' requires at least 4 arguments: id, color, type, and parameters.");
        }

        string id = tokens[0];
        string colorRaw = tokens[1];
        string type = tokens[2];
        string paramsString = tokens[3];

        Color color = ColorParser.Parse(colorRaw);

        IShapeGeometry geometry = _registry.Create(type, paramsString);

        Shape shape = new(id, color, geometry);

        _picture.AddShape(shape);
        return $"Added {type} '{id}' with parameters: {paramsString}";
    }
}
