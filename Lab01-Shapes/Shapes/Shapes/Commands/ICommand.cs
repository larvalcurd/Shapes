namespace Shapes.Commands;

public interface ICommand
{
    void Execute(string argsRaw);
}