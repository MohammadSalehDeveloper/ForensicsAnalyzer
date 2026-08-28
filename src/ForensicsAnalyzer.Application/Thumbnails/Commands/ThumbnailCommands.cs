using MediatR;

namespace ForensicsAnalyzer.Application.Thumbnails.Commands;

public record CreateThumbnailCommand(
    Guid FileCustomId,
    string ThumbnailPath,
    int Width,
    int Height,
    string? SizeLabel
) : IRequest<Guid>;

public record UpdateThumbnailCommand(
    Guid Id,
    string ThumbnailPath,
    int Width,
    int Height,
    string? SizeLabel
) : IRequest;

public record DeleteThumbnailCommand(Guid Id) : IRequest;
