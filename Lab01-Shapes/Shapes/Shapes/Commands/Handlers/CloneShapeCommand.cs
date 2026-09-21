namespace Shapes.Commands.Handlers;

public class CloneShapeCommand : ICommandHandler
{
    public void Execute(string argsRaw, CommandContext ctx)
    {
        var parts = argsRaw.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        var id = parts[0];
        var newId = parts[1];

        var original = ctx.Picture.GetShape(id);
        var clone = original.Clone(newId);
        
        ctx.Picture.AddShape(clone);
    }
}