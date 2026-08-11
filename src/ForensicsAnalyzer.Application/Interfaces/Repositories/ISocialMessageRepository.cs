using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Application.Interfaces.Repositories;

public interface ISocialMessageRepository
{
    Task AddAsync(SocialMessage entity, CancellationToken ct);
    Task<SocialMessage?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<SocialMessage>> GetByChatIdAsync(Guid chatId, CancellationToken ct);
    Task UpdateAsync(SocialMessage entity, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
