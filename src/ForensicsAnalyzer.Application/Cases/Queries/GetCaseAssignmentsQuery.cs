using ForensicsAnalyzer.Contracts.Cases;
using MediatR;

namespace ForensicsAnalyzer.Application.Cases.Queries;

public record GetCaseAssignmentsQuery(Guid CaseId) : IRequest<List<CaseAssignmentDto>>;
