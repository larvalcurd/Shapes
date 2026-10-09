namespace Shapes.Commands;

public class CommandDispatcher(TextWriter output)
{
    private readonly Dictionary<string, ICommand> _commands = new(StringComparer.OrdinalIgnoreCase);
    private readonly TextWriter _output = output ?? throw new ArgumentNullException(nameof(output));

    public void Register(string name, ICommand command)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Command name cannot be empty.", nameof(name));

        _commands[name] = command ?? throw new ArgumentNullException(nameof(command));
    }


    public void Dispatch(string line)
    {
        if (string.IsNullOrWhiteSpace(line)) return;

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
            var result = command.Execute(rest);

            if (!string.IsNullOrEmpty(result))
            {
                _output.WriteLine(result);
            }
        }
        catch (Exception ex)when (ex is ArgumentException 
                                      or InvalidOperationException 
                                      or FormatException 
                                      or OverflowException)
        {
            _output.WriteLine($"Error: {ex.Message}");
        }
    }
}
