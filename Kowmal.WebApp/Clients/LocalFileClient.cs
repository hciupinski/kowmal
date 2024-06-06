using Kowmal.WebApp.Clients.Interfaces;

namespace Kowmal.WebApp.Clients;

public class LocalFileClient : IFileClient
{
    private readonly string _webPath;
    public LocalFileClient(IConfiguration configuration)
    {
        _webPath = configuration.GetSection("WebPath").Value ?? throw new InvalidOperationException("WebPath is not defined");
    }
    public async Task<string> UploadFileAsync(byte[] content, string path, CancellationToken cancellationToken)
    {
        var localPath = Path.Combine(_webPath, path);
        Directory.CreateDirectory(Path.GetDirectoryName(localPath)!);
        await using var stream = new FileStream(localPath, FileMode.Create);
        await stream.WriteAsync(content, cancellationToken);

        return path;
    }

    public async Task<byte[]> ReadFileAsync(string path, CancellationToken cancellationToken)
    {
        var localPath = Path.Combine(_webPath, path);
        return await File.ReadAllBytesAsync(localPath, cancellationToken);
    }
}