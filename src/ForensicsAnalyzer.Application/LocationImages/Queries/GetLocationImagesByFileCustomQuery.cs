using ForensicsAnalyzer.Contracts.LocationImages;
using MediatR;

namespace ForensicsAnalyzer.Application.LocationImages.Queries;

public record GetLocationImagesByFileCustomQuery(Guid FileCustomId) : IRequest<List<LocationImageDto>>;
