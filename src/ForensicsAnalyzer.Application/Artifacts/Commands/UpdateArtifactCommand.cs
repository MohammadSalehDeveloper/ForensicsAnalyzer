using ForensicsAnalyzer.Domain.Enums;
using MediatR;

namespace ForensicsAnalyzer.Application.Artifacts.Commands;

public record UpdateArtifactCommand(
    Guid Id,
    string Name,
    ArtifactType Type
) : IRequest;
