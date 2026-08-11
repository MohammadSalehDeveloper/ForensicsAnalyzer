using ForensicsAnalyzer.Contracts.Sources;
using MediatR;

namespace ForensicsAnalyzer.Application.Sources.Queries;

public record GetSourcesQuery : IRequest<List<SourceDto>>;
