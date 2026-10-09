using Shapes.Shapes;

namespace Shapes.Commands.Handlers;

public class CloneShapeCommand(Picture picture) : ICommand
{
    private readonly Picture _picture = picture ?? throw new ArgumentNullException(nameof(picture));

    public string Execute(string args)
    {
        var tokens = args.Split([' '], StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length != 2)
        {
            throw new ArgumentException("Command 'CloneShape' requires exactly 2 arguments: source_id and new_id.");
        }

        var id = tokens[0];
        var newId = tokens[1];

        _picture.CloneShape(id, newId);
        return $"Cloned shape '{id}' to '{newId}'";
    }
}
