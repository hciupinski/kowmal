using MediatR;

namespace Kowmal.API.Features.UpdatePhotosStore;

public record PhotosUploaded() : INotification;