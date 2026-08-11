using ForensicsAnalyzer.Application.Interfaces.Repositories;
using ForensicsAnalyzer.Domain.Entities;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Repositories;

public class ArtifactRepository : IArtifactRepository
{
    private readonly ApplicationDbContext _context;
    public ArtifactRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(Artifact entity, CancellationToken ct)
    {
        _context.Artifacts.Add(entity);
        await _context.SaveChangesAsync(ct);
    }

    public Task<Artifact?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _context.Artifacts.FirstOrDefaultAsync(a => a.Id == id, ct);

    public async Task<IReadOnlyList<Artifact>> GetAllAsync(CancellationToken ct) =>
        await _context.Artifacts.AsNoTracking().ToListAsync(ct);

    public async Task<IReadOnlyList<Artifact>> GetByCaseIdAsync(Guid caseId, CancellationToken ct) =>
        await _context.Artifacts.AsNoTracking()
            .Where(a => a.CaseId == caseId).ToListAsync(ct);

    public async Task UpdateAsync(Artifact entity, CancellationToken ct)
    {
        _context.Artifacts.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _context.Artifacts.FindAsync([id], ct);
        if (entity != null)
        {
            _context.Artifacts.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
