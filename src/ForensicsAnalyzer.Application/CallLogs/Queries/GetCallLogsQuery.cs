using ForensicsAnalyzer.Contracts.CallLogs;
using MediatR;

namespace ForensicsAnalyzer.Application.CallLogs.Queries;

public record GetCallLogsQuery(Guid ArtifactId) : IRequest<List<CallLogDto>>;
