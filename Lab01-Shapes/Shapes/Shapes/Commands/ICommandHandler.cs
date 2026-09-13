namespace Shapes.Commands;

public interface ICommandHandler
{
    void Execute(string argsRaw, CommandContext ctx);
}