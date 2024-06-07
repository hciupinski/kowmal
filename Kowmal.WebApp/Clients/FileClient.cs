using Kowmal.WebApp.Clients.Interfaces;

namespace Kowmal.WebApp.Clients;

public class FileClient : IFileClient
{
    private readonly IBlobClient _blobClient;
    public FileClient(IBlobClient blobClient)
    {
        _blobClient = blobClient;
    }
    
    public async Task<string> UploadFileAsync(byte[] content, string path, CancellationToken cancellationToken)
    {
        using var stream = new MemoryStream(content);
        
        await _blobClient.UploadFileAsync(stream, path, cancellationToken);

        return path;
    }

    public Task<byte[]> ReadFileAsync(string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}