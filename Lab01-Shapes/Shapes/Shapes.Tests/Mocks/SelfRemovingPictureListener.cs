using Shapes.Core;
using Shapes.Core.Listeners;

namespace Shapes.Tests.Mocks;

public class SelfRemovingPictureListener : IPictureListener
{
    public void OnPictureChanged(Picture picture)
    {
        picture.RemoveListener(this);
    }
}