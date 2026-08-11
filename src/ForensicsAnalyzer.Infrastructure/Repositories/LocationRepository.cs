using ForensicsAnalyzer.Application.Interfaces.Repositories;
using ForensicsAnalyzer.Domain.Entities;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Repositories;

public class LocationRepository : ILocationRepository
{
    private readonly ApplicationDbContext _context;
    public LocationRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(Location entity, CancellationToken ct)
    {
        _context.Locations.Add(entity);
        await _context.SaveChangesAsync(ct);
    }

    public Task<Location?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _context.Locations.FirstOrDefaultAsync(l => l.Id == id, ct);

    public async Task<IReadOnlyList<Location>> GetByArtifactIdAsync(Guid artifactId, CancellationToken ct) =>
        await _context.Locations.AsNoTracking()
            .Where(l => l.ArtifactId == artifactId).ToListAsync(ct);

    public async Task UpdateAsync(Location entity, CancellationToken ct)
    {
        _context.Locations.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _context.Locations.FindAsync([id], ct);
        if (entity != null)
        {
            _context.Locations.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
