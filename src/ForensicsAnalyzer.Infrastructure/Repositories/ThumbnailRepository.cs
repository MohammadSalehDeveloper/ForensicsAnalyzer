using ForensicsAnalyzer.Application.Interfaces.Repositories;
using ForensicsAnalyzer.Domain.Entities;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Repositories;

public class ThumbnailRepository : IThumbnailRepository
{
    private readonly ApplicationDbContext _context;
    public ThumbnailRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(Thumbnail entity, CancellationToken ct)
    {
        _context.Thumbnails.Add(entity);
        await _context.SaveChangesAsync(ct);
    }

    public Task<Thumbnail?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _context.Thumbnails.FirstOrDefaultAsync(t => t.Id == id, ct);

    public async Task<IReadOnlyList<Thumbnail>> GetByFileCustomIdAsync(Guid fileCustomId, CancellationToken ct) =>
        await _context.Thumbnails.AsNoTracking()
            .Where(t => t.FileCustomId == fileCustomId).ToListAsync(ct);

    public async Task UpdateAsync(Thumbnail entity, CancellationToken ct)
    {
        _context.Thumbnails.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _context.Thumbnails.FindAsync([id], ct);
        if (entity != null)
        {
            _context.Thumbnails.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
