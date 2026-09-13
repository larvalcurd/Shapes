using Shapes.Formatting;

namespace Shapes.Commands.Handlers;

public class MovePictureCommand : ICommandHandler
{
    public void Execute(string argsRaw, CommandContext ctx)
    {
        var parts = argsRaw.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        var dx = NumberFormat.Parse(parts[0]);
        var dy = NumberFormat.Parse(parts[1]);
        ctx.Picture.Move(dx, dy);
    }
}