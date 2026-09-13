namespace Shapes.Commands.Handlers;

public class DrawPictureCommand : ICommandHandler
{
    public void Execute(string argsRaw, CommandContext ctx)
    {
        ctx.Picture.Draw(ctx.Canvas);
    }
}