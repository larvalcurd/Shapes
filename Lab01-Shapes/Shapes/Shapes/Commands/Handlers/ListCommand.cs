using Shapes.Shapes;

namespace Shapes.Commands.Handlers;

public class ListCommand(Picture picture, TextWriter output) : ICommand
{
    private readonly Picture _picture = picture ?? throw new ArgumentNullException(nameof(picture));
    private readonly TextWriter _output = output ?? throw new ArgumentNullException(nameof(output));

    public string Execute(string args)
    {
        var tokens = args.Split([' '], StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length > 0)
        {
            throw new ArgumentException("Command 'List' does not accept any arguments.");
        }

        var index = 1;
        var lines = new List<string>();

        foreach (var shape in _picture.GetShapes())
        {
            var typeName = shape.GetTypeName();
            var id = shape.Id;
            var colorString = shape.Color.ToString();
            var paramsString = shape.GetParamsString();

            var line = string.IsNullOrWhiteSpace(paramsString)
                ? $"{index} {typeName} {id} {colorString}"
                : $"{index} {typeName} {id} {colorString} {paramsString}";

            lines.Add(line);
            index++;
        }
        
        return string.Join(Environment.NewLine, lines);
    }
}