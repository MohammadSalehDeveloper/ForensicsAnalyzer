using MediatR;

namespace ForensicsAnalyzer.Application.Cases.Commands.CreateCase;

public record CreateCaseCommand(
    string Name,
    string Description,
    Guid SourceId
) : IRequest<Guid>;
