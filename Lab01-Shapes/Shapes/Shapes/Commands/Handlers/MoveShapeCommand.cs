using Shapes.Formatting;

namespace Shapes.Commands.Handlers;

public class MoveShapeCommand : ICommandHandler
{
    public void Execute(string argsRaw, CommandContext ctx)
    {
        var parts = argsRaw.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
        var id = parts[0];
        var dx =  NumberFormat.Parse(parts[1]);
        var dy =  NumberFormat.Parse(parts[2]);
        ctx.Picture.GetShape(id).Move(dx, dy);
    }
}