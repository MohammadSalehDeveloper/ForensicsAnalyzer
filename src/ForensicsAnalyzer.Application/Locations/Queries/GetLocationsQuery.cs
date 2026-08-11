using ForensicsAnalyzer.Contracts.Locations;
using MediatR;

namespace ForensicsAnalyzer.Application.Locations.Queries;

public record GetLocationsQuery(Guid ArtifactId) : IRequest<List<LocationDto>>;
