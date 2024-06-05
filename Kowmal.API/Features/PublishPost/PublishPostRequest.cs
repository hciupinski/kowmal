using MediatR;

namespace Kowmal.API.Features.PublishPost;

public record PublishPostRequest(Guid Identifier) : IRequest;