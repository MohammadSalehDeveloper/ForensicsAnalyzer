using ForensicsAnalyzer.Application.Interfaces.Repositories;
using ForensicsAnalyzer.Domain.Entities;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Repositories;

public class SocialChatRepository : ISocialChatRepository
{
    private readonly ApplicationDbContext _context;
    public SocialChatRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(SocialChat entity, CancellationToken ct)
    {
        _context.SocialChats.Add(entity);
        await _context.SaveChangesAsync(ct);
    }

    public Task<SocialChat?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _context.SocialChats.FirstOrDefaultAsync(c => c.Id == id, ct);

    public async Task<IReadOnlyList<SocialChat>> GetByMessengerIdAsync(Guid messengerId, CancellationToken ct) =>
        await _context.SocialChats.AsNoTracking()
            .Where(c => c.MessengerId == messengerId).ToListAsync(ct);

    public async Task UpdateAsync(SocialChat entity, CancellationToken ct)
    {
        _context.SocialChats.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _context.SocialChats.FindAsync([id], ct);
        if (entity != null)
        {
            _context.SocialChats.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
