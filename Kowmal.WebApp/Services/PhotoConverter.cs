using Kowmal.WebApp.Services.Interfaces;
using SkiaSharp;

namespace Kowmal.WebApp.Services;

public class PhotoConverter : IPhotoConverter
{
    public PhotoConvertModel ScaleImage(byte[] imageBytes, int maxWidth, int maxHeight)
    {
        SKBitmap image = SKBitmap.Decode(imageBytes);
        var result = ProcessScaleImage(image, maxWidth, maxHeight);
        return new PhotoConvertModel(
            result.Item1,
            result.Item2,
            result.Item3
        );
    }
    
    public PhotoConvertModel ScaleImage(Stream imageStream, int maxWidth, int maxHeight)
    {
        SKBitmap image = SKBitmap.Decode(imageStream);

        var result = ProcessScaleImage(image, maxWidth, maxHeight);
        return new PhotoConvertModel(
            result.Item1,
            result.Item2,
            result.Item3
        );
    }

    public byte[] AddWatermark(byte[] imageBytes)
    {
        var image = SKBitmap.Decode(imageBytes);

        return ProcessAddWatermark(image);
    }
    
    public byte[] AddWatermark(Stream imagesStream)
    {
        var image = SKBitmap.Decode(imagesStream);

        return ProcessAddWatermark(image);
    }

    private (byte[], int, int) ProcessScaleImage(SKBitmap image, int maxWidth, int maxHeight)
    {
        var ratioX = (double)maxWidth / image.Width;
        var ratioY = (double)maxHeight / image.Height;
        var ratio = Math.Min(ratioX, ratioY);

        var newWidth = (int)(image.Width * ratio);
        var newHeight = (int)(image.Height * ratio);

        var info = new SKImageInfo(newWidth, newHeight);
        image = image.Resize(info, SKFilterQuality.High);

        using var ms = new MemoryStream();
        image.Encode(ms, SKEncodedImageFormat.Png, 100);
        return (ms.ToArray(), newWidth, newHeight);
    }

    private byte[] ProcessAddWatermark(SKBitmap image)
    {
        var info = new SKImageInfo(image.Width, image.Height);
        var canvas = new SKCanvas(image);

        var font = SKTypeface.FromFamilyName("Arial");
        var brush = new SKPaint
        {
            Typeface = font,
            TextSize = 64.0f,
            IsAntialias = true,
            Color = new SKColor(255, 255, 255, 120)
        };
        
        var watermarkPositionX = image.Width / 4;   
        var watermarkPositionY = image.Height / 3;

        canvas.DrawText("Tomasz Kowmal", watermarkPositionX, watermarkPositionY, brush);

        canvas.Flush();

        using var ms = new MemoryStream();
        image.Encode(ms, SKEncodedImageFormat.Png, 100);
        return ms.ToArray();
    }
    
}

public record PhotoConvertModel(byte[] Content, int Width, int Height);