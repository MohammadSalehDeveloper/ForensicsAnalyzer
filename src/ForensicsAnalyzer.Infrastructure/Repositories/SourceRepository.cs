using ForensicsAnalyzer.Application.Interfaces.Repositories;
using ForensicsAnalyzer.Domain.Entities;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Repositories;

public class SourceRepository : ISourceRepository
{
    private readonly ApplicationDbContext _context;
    public SourceRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(Source entity, CancellationToken ct)
    {
        _context.Sources.Add(entity);
        await _context.SaveChangesAsync(ct);
    }

    public Task<Source?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _context.Sources.FirstOrDefaultAsync(s => s.Id == id, ct);

    public async Task<IReadOnlyList<Source>> GetAllAsync(CancellationToken ct) =>
        await _context.Sources.AsNoTracking().ToListAsync(ct);

    public async Task UpdateAsync(Source entity, CancellationToken ct)
    {
        _context.Sources.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _context.Sources.FindAsync([id], ct);
        if (entity != null)
        {
            _context.Sources.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
