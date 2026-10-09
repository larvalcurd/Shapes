using Shapes.Shapes;

namespace Shapes.Commands.Handlers;

public class DeleteShapeCommand(Picture picture) : ICommand
{
    private readonly Picture _picture = picture ?? throw new ArgumentNullException(nameof(picture));

    public void Execute(string args)
    {
        var tokens = args.Split([' '], StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length != 1)
        {
            throw new ArgumentException("Command 'DeleteShape' requires exactly 1 argument: id.");
        }

        var id = tokens[0];

        _picture.DeleteShape(id);
    }
}
