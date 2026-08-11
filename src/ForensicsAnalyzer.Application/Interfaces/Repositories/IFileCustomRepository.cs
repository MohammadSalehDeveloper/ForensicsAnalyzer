using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Application.Interfaces.Repositories;

public interface IFileCustomRepository
{
    Task AddAsync(FileCustom entity, CancellationToken ct);
    Task<FileCustom?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<FileCustom>> GetAllAsync(CancellationToken ct);
    Task UpdateAsync(FileCustom entity, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
