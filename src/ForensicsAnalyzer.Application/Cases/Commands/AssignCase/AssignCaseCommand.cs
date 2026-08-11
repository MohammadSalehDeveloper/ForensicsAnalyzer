using MediatR;

namespace ForensicsAnalyzer.Application.Cases.Commands.AssignCase;

public record AssignCaseCommand(Guid CaseId, string UserId) : IRequest<Guid>;
