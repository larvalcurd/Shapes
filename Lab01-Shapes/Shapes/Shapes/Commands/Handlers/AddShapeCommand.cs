using Shapes.Shapes;

namespace Shapes.Commands.Handlers;

public class AddShapeCommand : ICommandHandler
{
    public void Execute(string argsRaw, CommandContext ctx)
    {
        var parts = argsRaw.Split(' ', 4, StringSplitOptions.RemoveEmptyEntries);
        var id = parts[0];
        var color = Gfx.Color.Parse(parts[1]);
        var type = parts[2];
        var paramsRaw = parts.Length > 3 ? parts[3] : "";

        var geometry = ctx.Factory.Create(type, paramsRaw);
        ctx.Picture.AddShape(new Shape(id, color, geometry));
    }
}