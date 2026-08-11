using ForensicsAnalyzer.Contracts.Thumbnails;
using MediatR;

namespace ForensicsAnalyzer.Application.Thumbnails.Queries;

public record GetThumbnailsByFileCustomQuery(Guid FileCustomId) : IRequest<List<ThumbnailDto>>;
