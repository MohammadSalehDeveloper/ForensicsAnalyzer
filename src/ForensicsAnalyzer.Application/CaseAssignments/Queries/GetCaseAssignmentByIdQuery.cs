using ForensicsAnalyzer.Contracts.Cases;
using MediatR;

namespace ForensicsAnalyzer.Application.CaseAssignments.Queries;

public record GetCaseAssignmentByIdQuery(Guid Id) : IRequest<CaseAssignmentDto?>;
