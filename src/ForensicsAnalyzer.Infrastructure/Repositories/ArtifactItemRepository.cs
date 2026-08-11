using ForensicsAnalyzer.Application.Interfaces.Repositories;
using ForensicsAnalyzer.Domain.Entities;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Repositories;

public class ArtifactItemRepository : IArtifactItemRepository
{
    private readonly ApplicationDbContext _context;
    public ArtifactItemRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(ArtifactItem entity, CancellationToken ct)
    {
        _context.ArtifactItems.Add(entity);
        await _context.SaveChangesAsync(ct);
    }

    public Task<ArtifactItem?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _context.ArtifactItems.FirstOrDefaultAsync(i => i.Id == id, ct);

    public async Task<IReadOnlyList<ArtifactItem>> GetByArtifactIdAsync(Guid artifactId, CancellationToken ct) =>
        await _context.ArtifactItems.AsNoTracking()
            .Where(i => i.ArtifactId == artifactId).ToListAsync(ct);

    public async Task UpdateAsync(ArtifactItem entity, CancellationToken ct)
    {
        _context.ArtifactItems.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _context.ArtifactItems.FindAsync([id], ct);
        if (entity != null)
        {
            _context.ArtifactItems.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
