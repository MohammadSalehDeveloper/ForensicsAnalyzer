using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Application.Interfaces.Repositories;

public interface ISocialMessengerRepository
{
    Task AddAsync(SocialMessenger entity, CancellationToken ct);
    Task<SocialMessenger?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<SocialMessenger>> GetAllAsync(CancellationToken ct);
    Task UpdateAsync(SocialMessenger entity, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
