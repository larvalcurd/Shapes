using Shapes.Formatting;
using Shapes.Shapes;

namespace Shapes.Commands.Handlers;

public class MovePictureCommand(Picture picture) : ICommand
{
    private readonly Picture _picture = picture ?? throw new ArgumentNullException(nameof(picture));

    public void Execute(string args)
    {
        var tokens = args.Split([' '], StringSplitOptions.RemoveEmptyEntries);
        
        if (tokens.Length != 2)
        {
            throw new ArgumentException("Command 'MovePicture' requires exactly 2 arguments: dx and dy.");
        }
        
        var dx = NumberFormat.Parse(tokens[0]);
        var dy = NumberFormat.Parse(tokens[1]);
        
        _picture.MovePicture(dx, dy);
    }
}