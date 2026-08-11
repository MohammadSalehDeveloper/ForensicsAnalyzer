using MediatR;

namespace ForensicsAnalyzer.Application.Cases.Commands.UnassignCase;

public record UnassignCaseCommand(Guid AssignmentId) : IRequest;
