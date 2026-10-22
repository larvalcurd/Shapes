using Shapes.Core;

namespace Shapes.Commands.Handlers;

public class ListCommand(Picture picture, TextWriter output) : ICommand
{
    private readonly TextWriter _output = output ?? throw new ArgumentNullException(nameof(output));
    private readonly Picture _picture = picture ?? throw new ArgumentNullException(nameof(picture));

    public string Execute(string args)
    {
        string[] tokens = args.Split([' '], StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length > 0)
        {
            throw new ArgumentException("Command 'List' does not accept any arguments.");
        }

        int index = 1;
        List<string> lines = [];

        foreach (Shape shape in _picture.GetShapes())
        {
            string typeName = shape.GetTypeName();
            string id = shape.Id;
            string colorString = shape.Color.ToString();
            string paramsString = shape.GetParamsString();

            string line = string.IsNullOrWhiteSpace(paramsString)
                ? $"{index} {typeName} {id} {colorString}"
                : $"{index} {typeName} {id} {colorString} {paramsString}";

            lines.Add(line);
            index++;
        }

        return string.Join(Environment.NewLine, lines);
    }
}
