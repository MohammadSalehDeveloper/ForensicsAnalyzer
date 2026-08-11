using MediatR;

namespace ForensicsAnalyzer.Application.Locations.Commands;

public record DeleteLocationCommand(Guid Id) : IRequest;
