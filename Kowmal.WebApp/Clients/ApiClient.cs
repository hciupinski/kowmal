using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Kowmal.WebApp.Models;

namespace Kowmal.WebApp.Clients;

public interface IApiClient
{
    Task<Guid> CreatePostAsync(CreatePostRequest request, string token, CancellationToken cancellationToken = default);
    //Task<string> GetAntiforgeryToken(CancellationToken cancellationToken = default);
    Task UploadPhotosAsync(UploadPhotosRequest request, string token, CancellationToken cancellationToken = default);
    Task PublishPostAsync(PublishPostRequest identifier, CancellationToken cancellationToken = default);
    Task<List<PostListItemViewModel>> GetPostsAsync(CancellationToken cancellationToken = default);
}

public class ApiClient : IApiClient
{
    private readonly HttpClient _client;
    public ApiClient(HttpClient client, IConfiguration configuration)
    {
        _client = client;
        var url = configuration["BackendUrl"];
        
        _client.BaseAddress = new Uri(url ?? throw new InvalidOperationException("BackendUrl is not defined"));
    }
    
    public async Task<string> GetAntiforgeryToken(CancellationToken cancellationToken = default)
    {
        return await _client.GetStringAsync("api/antiforgery/token", cancellationToken) ?? throw new Exception();
    }
    
    public async Task<List<PostListItemViewModel>> GetPostsAsync(CancellationToken cancellationToken = default)
    {
        return await _client.GetFromJsonAsync<List<PostListItemViewModel>>("api/posts", cancellationToken) ?? throw new Exception();
    }
    
    public async Task<Guid> CreatePostAsync(CreatePostRequest request, string token, CancellationToken cancellationToken = default)
    {
        var response = await _client.PostAsJsonAsync("api/posts/create", request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception();
        }

        var post = await response.Content.ReadFromJsonAsync<PostViewModel>(cancellationToken);
        return post!.Identifier;
    }
    
    public async Task UploadPhotosAsync(UploadPhotosRequest request, string token, CancellationToken cancellationToken = default)
    {
        using var content = new MultipartFormDataContent();

        if (request.Files != null && !request.Files.Any())
        {
            throw new ArgumentNullException(nameof(request.Files),"Cannot create post without at least 1 photo.");
        }

        foreach (var file in request.Files!)
        {
            var fileContent = new StreamContent(file.OpenReadStream(maxAllowedSize: 10_000_000));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
            content.Add(fileContent, "files", file.Name);
        }

        var response = await _client.PostAsync($"api/posts/{request.Identifier}/photos/upload", content, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception();
        }
    }

    public async Task PublishPostAsync(PublishPostRequest request, CancellationToken cancellationToken = default)
    {
        var response = await _client.PostAsJsonAsync($"api/posts/{request.Identifier}/publish", request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception();
        }
    }
}