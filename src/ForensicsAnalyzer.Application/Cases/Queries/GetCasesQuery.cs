using ForensicsAnalyzer.Contracts.Cases;
using MediatR;

namespace ForensicsAnalyzer.Application.Cases.Queries;

public record GetCasesQuery : IRequest<List<CaseDto>>;