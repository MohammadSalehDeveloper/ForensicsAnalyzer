using MediatR;

namespace ForensicsAnalyzer.Application.LocationImages.Commands;

public record CreateLocationImageCommand(
    Guid FileCustomId,
    double Latitude,
    double Longitude,
    string? Description,
    bool IsPrimary,
    int Order
) : IRequest<Guid>;

public record UpdateLocationImageCommand(
    Guid Id,
    double Latitude,
    double Longitude,
    string? Description,
    bool IsPrimary,
    int Order
) : IRequest;

public record DeleteLocationImageCommand(Guid Id) : IRequest;
