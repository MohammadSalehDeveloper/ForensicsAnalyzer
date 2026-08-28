using ForensicsAnalyzer.Contracts.Artifacts;
using MediatR;

namespace ForensicsAnalyzer.Application.Artifacts.Queries;

public record GetArtifactsQuery(Guid CaseId) : IRequest<List<ArtifactDto>>;
