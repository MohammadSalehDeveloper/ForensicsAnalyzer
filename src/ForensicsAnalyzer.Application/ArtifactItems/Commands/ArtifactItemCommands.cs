using ForensicsAnalyzer.Domain.Enums;
using MediatR;

namespace ForensicsAnalyzer.Application.ArtifactItems.Commands;

public record CreateArtifactItemCommand(
    Guid ArtifactId,
    ArtifactType Type,
    Guid ReferenceId
) : IRequest<Guid>;

public record DeleteArtifactItemCommand(Guid Id) : IRequest;
