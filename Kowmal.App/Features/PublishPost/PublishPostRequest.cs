using MediatR;

namespace Kowmal.App.Features.PublishPost;

public record PublishPostRequest(Guid Identifier) : IRequest;