using MediatR;

namespace ForensicsAnalyzer.Application.CallLogs.Commands;

public record DeleteCallLogCommand(Guid Id) : IRequest;
