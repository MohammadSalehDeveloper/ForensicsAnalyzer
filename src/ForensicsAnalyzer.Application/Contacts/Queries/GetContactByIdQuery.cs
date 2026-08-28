using ForensicsAnalyzer.Contracts.Contacts;
using MediatR;

namespace ForensicsAnalyzer.Application.Contacts.Queries;

public record GetContactByIdQuery(Guid Id) : IRequest<ContactDto?>;
