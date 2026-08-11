using ForensicsAnalyzer.Application.Interfaces.Repositories;
using ForensicsAnalyzer.Domain.Entities;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Repositories;

public class LocationImageRepository : ILocationImageRepository
{
    private readonly ApplicationDbContext _context;
    public LocationImageRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(LocationImage entity, CancellationToken ct)
    {
        _context.LocationImages.Add(entity);
        await _context.SaveChangesAsync(ct);
    }

    public Task<LocationImage?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _context.LocationImages.FirstOrDefaultAsync(li => li.Id == id, ct);

    public async Task<IReadOnlyList<LocationImage>> GetByFileCustomIdAsync(Guid fileCustomId, CancellationToken ct) =>
        await _context.LocationImages.AsNoTracking()
            .Where(li => li.FileCustomId == fileCustomId).ToListAsync(ct);

    public async Task UpdateAsync(LocationImage entity, CancellationToken ct)
    {
        _context.LocationImages.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _context.LocationImages.FindAsync([id], ct);
        if (entity != null)
        {
            _context.LocationImages.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
