namespace Shapes.Commands.Handlers;

public class ChangeColorCommand : ICommandHandler
{
    public void Execute(string argsRaw, CommandContext ctx)
    {
        var parts = argsRaw.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
        var id = parts[0];
        var color = Gfx.Color.Parse(parts[1]);
        ctx.Picture.GetShape(id).Color = color;
    }
}