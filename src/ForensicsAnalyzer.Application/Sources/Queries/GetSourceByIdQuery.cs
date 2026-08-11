using ForensicsAnalyzer.Contracts.Sources;
using MediatR;

namespace ForensicsAnalyzer.Application.Sources.Queries;

public record GetSourceByIdQuery(Guid Id) : IRequest<SourceDto?>;
