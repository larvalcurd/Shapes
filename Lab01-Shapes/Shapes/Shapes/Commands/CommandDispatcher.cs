namespace Shapes.Commands;

public class CommandDispatcher
{
    private readonly Dictionary<string, ICommand> _commands = new(StringComparer.OrdinalIgnoreCase);
    private readonly TextWriter _output;

    public CommandDispatcher(TextWriter output)
    {
        _output = output ?? throw new ArgumentNullException(nameof(output));
    }

    public void Register(string name, ICommand command)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("The command name cannot be empty.", nameof(name));
        _commands[name] = command ?? throw new ArgumentNullException(nameof(command));
    }
    

    public void Dispatch(string line)
    {
        line = line.Trim();
        if (line.Length == 0) return;
        
        var spaceIndex = line.IndexOf(' ');
        var commandName = spaceIndex < 0 ? line : line[..spaceIndex];
        var rest = spaceIndex < 0 ? "" : line[(spaceIndex + 1)..];
        
        if (!_commands.TryGetValue(commandName, out var command))
        {
            _output.WriteLine($"Error: unknown command '{commandName}'");
            return;
        }

        try
        {
            command.Execute(rest);
        }
        catch (Exception ex)
        {
            _output.WriteLine($"Error: {ex.Message}");
        }
    }
}


