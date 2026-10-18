using Shapes.Core;

namespace Shapes.Tests.Mocks;

public class FakePictureListener : IPictureListener
{
    public int CallCount { get; private set; }
    public Picture? LastNotifiedPicture { get; private set; }

    public void OnPictureChanged(Picture picture)
    {
        CallCount++;
        LastNotifiedPicture = picture;
    }
}
