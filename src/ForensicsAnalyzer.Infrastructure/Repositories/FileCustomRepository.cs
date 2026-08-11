using ForensicsAnalyzer.Application.Interfaces.Repositories;
using ForensicsAnalyzer.Domain.Entities;
using ForensicsAnalyzer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Infrastructure.Repositories;

public class FileCustomRepository : IFileCustomRepository
{
    private readonly ApplicationDbContext _context;
    public FileCustomRepository(ApplicationDbContext context) => _context = context;

    public async Task AddAsync(FileCustom entity, CancellationToken ct)
    {
        _context.FileCustoms.Add(entity);
        await _context.SaveChangesAsync(ct);
    }

    public Task<FileCustom?> GetByIdAsync(Guid id, CancellationToken ct) =>
        _context.FileCustoms.FirstOrDefaultAsync(f => f.Id == id, ct);

    public async Task<IReadOnlyList<FileCustom>> GetAllAsync(CancellationToken ct) =>
        await _context.FileCustoms.AsNoTracking().ToListAsync(ct);

    public async Task UpdateAsync(FileCustom entity, CancellationToken ct)
    {
        _context.FileCustoms.Update(entity);
        await _context.SaveChangesAsync(ct);
    }

    public async Task DeleteAsync(Guid id, CancellationToken ct)
    {
        var entity = await _context.FileCustoms.FindAsync([id], ct);
        if (entity != null)
        {
            _context.FileCustoms.Remove(entity);
            await _context.SaveChangesAsync(ct);
        }
    }
}
