using ForensicsAnalyzer.Domain.Enums;
using MediatR;

namespace ForensicsAnalyzer.Application.Artifacts.Commands;

public record CreateArtifactCommand(
    Guid CaseId,
    string Name,
    ArtifactType Type
) : IRequest<Guid>;
