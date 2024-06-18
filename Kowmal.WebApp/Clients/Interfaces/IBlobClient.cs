namespace Kowmal.WebApp.Clients.Interfaces;

public interface IBlobClient
{
    Task UploadFileAsync(Stream content, string path, CancellationToken cancellationToken);
}