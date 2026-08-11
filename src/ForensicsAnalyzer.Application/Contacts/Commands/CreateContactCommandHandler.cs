using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Entities;
using MediatR;

namespace ForensicsAnalyzer.Application.Contacts.Commands;

public sealed class CreateContactCommandHandler : IRequestHandler<CreateContactCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    public CreateContactCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Guid> Handle(CreateContactCommand r, CancellationToken ct)
    {
        var entity = new Contact
        {
            ArtifactId = r.ArtifactId, FirstName = r.FirstName, LastName = r.LastName,
            Alias = r.Alias, PhoneNumber = r.PhoneNumber, Email = r.Email,
            Address = r.Address, Organization = r.Organization, JobTitle = r.JobTitle,
            SocialMediaHandle = r.SocialMediaHandle, Notes = r.Notes, Source = r.Source,
            LastContactedAt = r.LastContactedAt, IsSuspicious = r.IsSuspicious
        };
        _context.Contacts.Add(entity);
        await _context.SaveChangesAsync(ct);
        return entity.Id;
    }
}
