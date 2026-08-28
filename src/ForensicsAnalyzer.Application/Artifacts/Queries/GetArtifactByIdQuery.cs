using ForensicsAnalyzer.Contracts.Artifacts;
using MediatR;

namespace ForensicsAnalyzer.Application.Artifacts.Queries;

public record GetArtifactByIdQuery(Guid Id) : IRequest<ArtifactDto?>;
