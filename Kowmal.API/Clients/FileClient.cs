using Kowmal.API.Clients.Interfaces;

namespace Kowmal.API.Clients;

public class FileClient : IFileClient
{
    public async Task<string> UploadFileAsync(byte[] content, string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public async Task<byte[]> ReadFileAsync(string path, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}