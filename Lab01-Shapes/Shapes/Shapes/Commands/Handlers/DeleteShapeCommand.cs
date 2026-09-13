namespace Shapes.Commands.Handlers;

public class DeleteShapeCommand : ICommandHandler
{
    public void Execute(string argsRaw, CommandContext ctx)
    {
        var parts = argsRaw.Split(' ', StringSplitOptions.RemoveEmptyEntries);
        var id = parts[0];
        ctx.Picture.DeleteShape(id);
    }
}