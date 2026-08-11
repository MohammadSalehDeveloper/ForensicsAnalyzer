using ForensicsAnalyzer.Contracts.Contacts;
using MediatR;

namespace ForensicsAnalyzer.Application.Contacts.Queries;

public record GetContactsQuery(Guid ArtifactId) : IRequest<List<ContactDto>>;
