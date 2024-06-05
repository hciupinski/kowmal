using Kowmal.API.Apis.ViewModels;
using Kowmal.API.Clients.Interfaces;
using Kowmal.API.Features.CreatePost;
using Kowmal.API.Features.PublishPost;
using Kowmal.API.Features.UploadPhotos;
using Kowmal.API.Models;
using Kowmal.API.Services.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Kowmal.API.Apis;

public static class PostsApi
{
    public static IEndpointRouteBuilder MapPostsApi(this IEndpointRouteBuilder app)
    {
        app.MapGet("/", GetPosts);
        app.MapGet("/{id:guid}", GetPost);
        // app.MapGet("/{id:guid}/photos/{photoId:guid}/thumb", GetPhotoThumb);

        app.MapPost("/create", CreatePost);
        app.MapPost("/{id:guid}/publish", PublishPost);
        
        app.MapPost("/{id:guid}/photos/upload", UploadPhotos).DisableAntiforgery();

        return app;
    }
    
    public static async Task<Results<Ok<List<PostListItem>>, BadRequest<string>>> GetPosts([FromServices] IPostService postService, CancellationToken cancellationToken)
    {
        var results = await postService.GetPostsAsync(cancellationToken);
        return TypedResults.Ok(results);
    }
    
    public static async Task<Results<Ok<Post>, BadRequest<string>>> GetPost([FromQuery] Guid id, [FromServices] IPostService postService, CancellationToken cancellationToken)
    {
        var result = await postService.GetPostAsync(id, cancellationToken);
        return result != null ? TypedResults.Ok(result) : TypedResults.BadRequest("not found");
    }
    
    public static async Task<Results<Created<Post>, BadRequest<string>>> CreatePost([FromBody] CreatePostRequest request, [FromServices] IMediator mediator, CancellationToken cancellationToken)
    {
        var post = await mediator.Send(request, cancellationToken);
        return TypedResults.Created($"{post.Identifier}", post);
    }
    
    public static async Task<Results<Ok, BadRequest<string>>> UploadPhotos([FromRoute] Guid id, [FromForm] List<IFormFile> files, [FromServices] IMediator mediator, HttpRequest httpRequest, CancellationToken cancellationToken)
    {
        var request = new UploadPhotosRequest() {Files = httpRequest.Form.Files, PostId = id};
        await mediator.Send(request, cancellationToken);
        return TypedResults.Ok();
    }
    
    public static async Task<Results<Ok, BadRequest<string>>> PublishPost([FromBody] PublishPostRequest request,  [FromServices] IMediator mediator, CancellationToken cancellationToken)
    {
        await mediator.Send(request, cancellationToken);
        return TypedResults.Ok();
    }

    // public static async Task<Results<FileContentHttpResult, BadRequest<string>>> GetPhotoThumb([FromQuery] Guid id, [FromQuery] Guid photoId, [FromServices] IFileClient fileClient, [FromRoute] string name, CancellationToken cancellationToken)
    // {
    //     throw new NotImplementedException();
    // }
}

