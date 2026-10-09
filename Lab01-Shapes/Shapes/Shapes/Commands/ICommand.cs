namespace Shapes.Commands;

public interface ICommand
{
    string Execute(string argsRaw);
}