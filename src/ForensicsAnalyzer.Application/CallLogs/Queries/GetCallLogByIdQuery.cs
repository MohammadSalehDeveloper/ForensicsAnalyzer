using ForensicsAnalyzer.Contracts.CallLogs;
using MediatR;

namespace ForensicsAnalyzer.Application.CallLogs.Queries;

public record GetCallLogByIdQuery(Guid Id) : IRequest<CallLogDto?>;
