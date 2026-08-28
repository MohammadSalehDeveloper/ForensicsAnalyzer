using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Contacts.Commands;

public sealed class UpdateContactCommandHandler : IRequestHandler<UpdateContactCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateContactCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(UpdateContactCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Contacts
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken)
            ?? throw new KeyNotFoundException($"Contact {request.Id} not found.");

        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.Alias = request.Alias;
        entity.PhoneNumber = request.PhoneNumber;
        entity.Email = request.Email;
        entity.Address = request.Address;
        entity.Organization = request.Organization;
        entity.JobTitle = request.JobTitle;
        entity.SocialMediaHandle = request.SocialMediaHandle;
        entity.Notes = request.Notes;
        entity.Source = request.Source;
        entity.LastContactedAt = request.LastContactedAt;
        entity.IsSuspicious = request.IsSuspicious;
        entity.UpdatedAt = DateTime.UtcNow;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
