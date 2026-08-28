using ForensicsAnalyzer.Contracts.LocationImages;
using MediatR;

namespace ForensicsAnalyzer.Application.LocationImages.Queries;

public record GetLocationImageByIdQuery(Guid Id) : IRequest<LocationImageDto?>;
