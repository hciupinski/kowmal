using Kowmal.WebApp.ViewModels;
using MediatR;

namespace Kowmal.WebApp.Features.GetPost;

public record GetPostQuery(Guid Identifier) : IRequest<PostViewModel>;