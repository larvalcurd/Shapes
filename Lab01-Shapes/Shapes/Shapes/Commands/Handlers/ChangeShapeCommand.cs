namespace Shapes.Commands.Handlers;

public class ChangeShapeCommand : ICommandHandler
{
    public void Execute(string argsRaw, CommandContext ctx)
    {
        var parts = argsRaw.Split(' ', 3, StringSplitOptions.RemoveEmptyEntries);
        var id = parts[0];
        var type = parts[1];
        var paramsRaw = parts.Length > 2 ? parts[2] : "";
        
        var shape = ctx.Picture.GetShape(id);
        var newGeometry = ctx.Factory.Create(type, paramsRaw);
        shape.Geometry = newGeometry;
    }
}