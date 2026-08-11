using MediatR;

namespace ForensicsAnalyzer.Application.Contacts.Commands;

public record DeleteContactCommand(Guid Id) : IRequest;
