using ForensicsAnalyzer.Contracts.Artifacts;
using MediatR;

namespace ForensicsAnalyzer.Application.ArtifactItems.Queries;

public record GetArtifactItemByIdQuery(Guid Id) : IRequest<ArtifactItemDto?>;
