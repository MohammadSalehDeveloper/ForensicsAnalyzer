using MediatR;

namespace ForensicsAnalyzer.Application.Artifacts.Commands;

public record DeleteArtifactCommand(Guid Id) : IRequest;
