using ForensicsAnalyzer.Application.Interfaces.Repositories;
using ForensicsAnalyzer.Domain.Entities;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Repositories;

public class SocialMessengerRepository : ISocialMessengerRepository
{
    private readonly ApplicationDbContext _context;
    public SocialMessengerRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(SocialMessenger entity, CancellationToken ct)
    {
        _context.SocialMessengers.Add(entity);
        await _context.SaveChangesAsync(ct);
    }

    public Task<SocialMessenger?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _context.SocialMessengers.FirstOrDefaultAsync(m => m.Id == id, ct);

    public async Task<IReadOnlyList<SocialMessenger>> GetAllAsync(CancellationToken ct) =>
        await _context.SocialMessengers.AsNoTracking().ToListAsync(ct);

    public async Task UpdateAsync(SocialMessenger entity, CancellationToken ct)
    {
        _context.SocialMessengers.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _context.SocialMessengers.FindAsync([id], ct);
        if (entity != null)
        {
            _context.SocialMessengers.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
