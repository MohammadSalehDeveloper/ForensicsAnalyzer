using ForensicsAnalyzer.Contracts.Thumbnails;
using MediatR;

namespace ForensicsAnalyzer.Application.Thumbnails.Queries;

public record GetThumbnailByIdQuery(Guid Id) : IRequest<ThumbnailDto?>;
