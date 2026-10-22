using Shapes.Core;
using Shapes.Formatting;

namespace Shapes.Commands.Handlers;

public class MovePictureCommand(Picture picture) : ICommand
{
    private readonly Picture _picture = picture ?? throw new ArgumentNullException(nameof(picture));

    public string Execute(string args)
    {
        string[] tokens = args.Split([' '], StringSplitOptions.RemoveEmptyEntries);

        if (tokens.Length != 2)
        {
            throw new ArgumentException("Command 'MovePicture' requires exactly 2 arguments: dx and dy.");
        }

        double dx = NumberFormat.Parse(tokens[0]);
        double dy = NumberFormat.Parse(tokens[1]);

        _picture.MovePicture(dx, dy);
        return $"Moved picture by dx: {dx}, dy: {dy}";
    }
}
