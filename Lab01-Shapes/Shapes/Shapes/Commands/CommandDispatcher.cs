namespace Shapes.Commands;

public class CommandDispatcher
{
    private readonly Dictionary<string, ICommandHandler> _handlers = new(StringComparer.OrdinalIgnoreCase);
    
    public void Register(string name, ICommandHandler handler) => _handlers[name] = handler;

    public void Dispatch(string line, CommandContext ctx)
    {
        line = line.Trim();
        if (line.Length == 0) return;
        
        var spaceIndex = line.IndexOf(' ');
        var commandName = spaceIndex < 0 ? line : line[..spaceIndex];
        var rest = spaceIndex < 0 ? "" : line[(spaceIndex + 1)..];

        if (!_handlers.TryGetValue(commandName, out var handler))
        {
            Console.WriteLine($"Error: unknown command '{commandName}'");
            return;
        }
        
        try
        {
            handler.Execute(rest, ctx);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }
}