using Shapes.Core;

namespace Shapes.Commands.Handlers;

public class DeleteShapeCommand(Picture picture) : ICommand
{
    private readonly Picture _picture = picture ?? throw new ArgumentNullException(nameof(picture));

    public string Execute(string args)
    {
        string[] tokens = args.Split([' '], StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length != 1)
        {
            throw new ArgumentException("Command 'DeleteShape' requires exactly 1 argument: id.");
        }

        string id = tokens[0];

        _picture.DeleteShape(id);
        return $"Deleted shape '{id}'";
    }
}
