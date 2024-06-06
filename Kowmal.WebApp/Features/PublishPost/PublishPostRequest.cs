using MediatR;

namespace Kowmal.WebApp.Features.PublishPost;

public record PublishPostRequest(Guid Identifier) : IRequest;