using ForensicsAnalyzer.Application.Interfaces.Repositories;
using ForensicsAnalyzer.Domain.Entities;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Repositories;

public class SocialMemberRepository : ISocialMemberRepository
{
    private readonly ApplicationDbContext _context;
    public SocialMemberRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(SocialMember entity, CancellationToken ct)
    {
        _context.SocialMembers.Add(entity);
        await _context.SaveChangesAsync(ct);
    }

    public Task<SocialMember?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _context.SocialMembers.FirstOrDefaultAsync(m => m.Id == id, ct);

    public async Task<IReadOnlyList<SocialMember>> GetByChatIdAsync(Guid chatId, CancellationToken ct) =>
        await _context.SocialMembers.AsNoTracking()
            .Where(m => m.ChatId == chatId).ToListAsync(ct);

    public async Task UpdateAsync(SocialMember entity, CancellationToken ct)
    {
        _context.SocialMembers.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _context.SocialMembers.FindAsync([id], ct);
        if (entity != null)
        {
            _context.SocialMembers.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
