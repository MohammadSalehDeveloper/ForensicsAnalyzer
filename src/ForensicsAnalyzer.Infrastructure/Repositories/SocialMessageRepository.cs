using ForensicsAnalyzer.Application.Interfaces.Repositories;
using ForensicsAnalyzer.Domain.Entities;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Repositories;

public class SocialMessageRepository : ISocialMessageRepository
{
    private readonly ApplicationDbContext _context;
    public SocialMessageRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(SocialMessage entity, CancellationToken ct)
    {
        _context.SocialMessages.Add(entity);
        await _context.SaveChangesAsync(ct);
    }

    public Task<SocialMessage?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _context.SocialMessages.FirstOrDefaultAsync(m => m.Id == id, ct);

    public async Task<IReadOnlyList<SocialMessage>> GetByChatIdAsync(Guid chatId, CancellationToken ct) =>
        await _context.SocialMessages.AsNoTracking()
            .Where(m => m.ChatId == chatId).ToListAsync(ct);

    public async Task UpdateAsync(SocialMessage entity, CancellationToken ct)
    {
        _context.SocialMessages.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _context.SocialMessages.FindAsync([id], ct);
        if (entity != null)
        {
            _context.SocialMessages.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
