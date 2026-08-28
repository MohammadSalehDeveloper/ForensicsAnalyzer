using ForensicsAnalyzer.Contracts.Locations;
using MediatR;

namespace ForensicsAnalyzer.Application.Locations.Queries;

public record GetLocationByIdQuery(Guid Id) : IRequest<LocationDto?>;
