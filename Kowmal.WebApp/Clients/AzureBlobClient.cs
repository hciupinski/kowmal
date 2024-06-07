using Azure.Storage.Blobs;
using Kowmal.WebApp.Clients.Interfaces;
using Kowmal.WebApp.Configuration;
using Microsoft.Extensions.Options;

namespace Kowmal.WebApp.Clients;

public class AzureBlobClient : IBlobClient
{
    private readonly BlobServiceClient _client;
    private readonly AzureStorageOptions _storageOptions;

    public AzureBlobClient(IOptions<AzureStorageOptions> options)
    {
        _storageOptions = options.Value;
        _client = new BlobServiceClient(_storageOptions.ConnectionString);
    }
    
    public async Task UploadFileAsync(Stream content, string path, CancellationToken cancellationToken)
    {
        var containerClient = _client.GetBlobContainerClient(_storageOptions.ContainerName);
        
        await containerClient.CreateIfNotExistsAsync(cancellationToken: cancellationToken);
        
        BlobClient blobClient = containerClient.GetBlobClient(path);
        
        await blobClient.UploadAsync(content, overwrite: true, cancellationToken);
    }
}