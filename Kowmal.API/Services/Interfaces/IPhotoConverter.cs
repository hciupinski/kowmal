namespace Kowmal.API.Services.Interfaces;

public interface IPhotoConverter
{
    PhotoConvertModel ScaleImage(byte[] imageBytes, int maxWidth, int maxHeight);
    PhotoConvertModel ScaleImage(Stream imageStream, int maxWidth, int maxHeight);
    byte[] AddWatermark(byte[] imageBytes);
    byte[] AddWatermark(Stream imagesStream);
}