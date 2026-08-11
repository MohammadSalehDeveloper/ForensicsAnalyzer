using ForensicsAnalyzer.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Contacts.Commands;

public sealed class DeleteContactCommandHandler : IRequestHandler<DeleteContactCommand>
{
    private readonly IApplicationDbContext _context;
    public DeleteContactCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task Handle(DeleteContactCommand request, CancellationToken ct)
    {
        var entity = await _context.Contacts
            .FirstOrDefaultAsync(c => c.Id == request.Id, ct)
            ?? throw new KeyNotFoundException($"Contact {request.Id} not found.");
        _context.Contacts.Remove(entity);
        await _context.SaveChangesAsync(ct);
    }
}
