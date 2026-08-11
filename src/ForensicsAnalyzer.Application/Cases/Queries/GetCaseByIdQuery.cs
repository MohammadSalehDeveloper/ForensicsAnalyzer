using ForensicsAnalyzer.Contracts.Cases;
using MediatR;

namespace ForensicsAnalyzer.Application.Cases.Queries;

public sealed record GetCaseByIdQuery(Guid Id) : IRequest<CaseDto?>;
