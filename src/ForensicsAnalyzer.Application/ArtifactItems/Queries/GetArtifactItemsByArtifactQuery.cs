using ForensicsAnalyzer.Contracts.Artifacts;
using MediatR;

namespace ForensicsAnalyzer.Application.ArtifactItems.Queries;

public record GetArtifactItemsByArtifactQuery(Guid ArtifactId) : IRequest<List<ArtifactItemDto>>;
