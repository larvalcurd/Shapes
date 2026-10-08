using Shapes.Formatting;
using Shapes.Shapes;

namespace Shapes.Commands.Handlers;

public class MoveShapeCommand(Picture picture) : ICommand
{
    private readonly Picture _picture = picture ?? throw new ArgumentNullException(nameof(picture));

    public void Execute(string args)
    {
        var tokens = args.Split([' '], StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length != 3)
        {
            throw new InvalidOperationException("Command 'MoveShape' requires exactly 3 arguments: id, dx, dy.");
        }
        
        var id = tokens[0];
        
        Shape? shape = _picture.GetShape(id);

        if (shape == null)
        {
            throw new InvalidOperationException($"Shape with id '{id}' does not exist.");
        }
        
        var dx = NumberFormat.Parse(tokens[1]);
        var dy = NumberFormat.Parse(tokens[2]);
        
        shape.Move(dx, dy);
    }
}