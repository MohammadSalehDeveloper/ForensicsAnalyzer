using ForensicsAnalyzer.Application.Interfaces.Repositories;
using ForensicsAnalyzer.Domain.Entities;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Repositories;

public class ContactRepository : IContactRepository
{
    private readonly ApplicationDbContext _context;
    public ContactRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(Contact entity, CancellationToken ct)
    {
        _context.Contacts.Add(entity);
        await _context.SaveChangesAsync(ct);
    }

    public Task<Contact?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _context.Contacts.FirstOrDefaultAsync(c => c.Id == id, ct);

    public async Task<IReadOnlyList<Contact>> GetByArtifactIdAsync(Guid artifactId, CancellationToken ct) =>
        await _context.Contacts.AsNoTracking()
            .Where(c => c.ArtifactId == artifactId).ToListAsync(ct);

    public async Task UpdateAsync(Contact entity, CancellationToken ct)
    {
        _context.Contacts.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _context.Contacts.FindAsync([id], ct);
        if (entity != null)
        {
            _context.Contacts.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
