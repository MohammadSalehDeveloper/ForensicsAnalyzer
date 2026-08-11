using MediatR;

namespace ForensicsAnalyzer.Application.Sources.Commands;

public record DeleteSourceCommand(Guid Id) : IRequest;
