using ForensicsAnalyzer.Contracts.Cases;
using MediatR;

namespace ForensicsAnalyzer.Application.CaseAssignments.Queries;

public record GetCaseAssignmentsByUserQuery(string UserId) : IRequest<List<CaseAssignmentDto>>;
