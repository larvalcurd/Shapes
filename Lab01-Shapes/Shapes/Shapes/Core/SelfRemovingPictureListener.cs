namespace Shapes.Core;

public class SelfRemovingPictureListener : IPictureListener
{
    public void OnPictureChanged(Picture picture)
    {
        picture.RemoveListener(this);
    }
}