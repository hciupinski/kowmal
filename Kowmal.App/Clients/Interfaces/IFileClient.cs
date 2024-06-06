namespace Kowmal.App.Clients.Interfaces;

public interface IFileClient
{
    Task<string> UploadFileAsync(byte[] content, string path, CancellationToken cancellationToken);
    Task<byte[]> ReadFileAsync(string path, CancellationToken cancellationToken);
}