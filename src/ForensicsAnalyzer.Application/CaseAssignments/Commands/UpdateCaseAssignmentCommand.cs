using MediatR;

namespace ForensicsAnalyzer.Application.CaseAssignments.Commands;

public record UpdateCaseAssignmentCommand(Guid Id, string UserId) : IRequest;
