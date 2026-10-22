using Shapes.Core;

namespace Shapes.Commands.Handlers;

public class ChangeShapeCommand(Picture picture, Registry registry) : ICommand
{
    private readonly Picture _picture = picture ?? throw new ArgumentNullException(nameof(picture));
    private readonly Registry _registry = registry ?? throw new ArgumentNullException(nameof(registry));

    public string Execute(string args)
    {
        string[] tokens = args.Split([' '], 3, StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length < 3)
        {
            throw new ArgumentException(
                "Command 'ChangeShape' requires at least 3 arguments: id, type, and parameters.");
        }

        string id = tokens[0];
        string type = tokens[1];
        string paramsString = tokens[2];

        Shape? shape = _picture.GetShape(id) ??
                       throw new InvalidOperationException($"Shape with id '{id}' does not exist.");

        IShapeGeometry newGeometry = _registry.Create(type, paramsString);

        shape.ReplaceGeometry(newGeometry);
        return $"Changed geometry of shape '{id}' to {type} with parameters: {paramsString}";
    }
}
