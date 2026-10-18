using Shapes.Core;
using Shapes.Gfx;
using Shapes.Observers;
using Shapes.Tests.Mocks;

namespace Shapes.Tests;

public class PictureLoggerTests
{
    [Fact]
    public void AddShape_NotifiesLogger_OutputsPictureChangedAndShapeDetails()
    {
        using var sw = new StringWriter();
        var logger = new PictureLogger(sw);
        var picture = new Picture();
        picture.AddListener(logger);

        picture.AddShape(new Shape("sh1", new Color(255, 0, 0), new FakeGeometry(paramsString: "10 20")));

        var output = sw.ToString();

        Assert.Contains("Picture changed:", output);
        Assert.Contains("sh1", output);
        Assert.Contains("#FF0000", output);
        Assert.Contains("FakeGeometry", output);
    }

    [Fact]
    public void OnPictureChanged_EmptyPicture_OutputsEmptyMessage()
    {
        using var sw = new StringWriter();
        var logger = new PictureLogger(sw);
        var picture = new Picture();

        logger.OnPictureChanged(picture);

        var output = sw.ToString();

        Assert.Contains("Picture changed:", output);
        Assert.Contains("Picture is empty", output);
    }

    [Fact]
    public void DeleteLastShape_NotifiesLoggerWithEmptyPicture()
    {
        using var sw = new StringWriter();
        var logger = new PictureLogger(sw);
        var picture = new Picture();
        var shape = new Shape("sh1", new Color(0, 0, 255), new FakeGeometry());
        picture.AddShape(shape);

        picture.AddListener(logger);
        picture.DeleteShape("sh1");

        var output = sw.ToString();

        Assert.Contains("Picture changed:", output);
        Assert.Contains("Picture is empty", output);
    }
}
