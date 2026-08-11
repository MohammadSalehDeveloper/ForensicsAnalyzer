using MediatR;

namespace ForensicsAnalyzer.Application.FileCustoms.Commands;

public record DeleteFileCustomCommand(Guid Id) : IRequest;
