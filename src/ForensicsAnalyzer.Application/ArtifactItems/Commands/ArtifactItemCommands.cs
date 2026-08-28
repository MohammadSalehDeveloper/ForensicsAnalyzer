using ForensicsAnalyzer.Domain.Enums;
using MediatR;

namespace ForensicsAnalyzer.Application.ArtifactItems.Commands;

public record CreateArtifactItemCommand(
    Guid ArtifactId,
    ArtifactType Type,
    Guid ReferenceId
) : IRequest<Guid>;

public record UpdateArtifactItemCommand(
    Guid Id,
    ArtifactType Type,
    Guid ReferenceId
) : IRequest;

public record DeleteArtifactItemCommand(Guid Id) : IRequest;
