using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Application.Interfaces.Repositories;

public interface ISocialMemberRepository
{
    Task AddAsync(SocialMember entity, CancellationToken ct);
    Task<SocialMember?> GetByIdAsync(Guid id, CancellationToken ct);
    Task<IReadOnlyList<SocialMember>> GetByChatIdAsync(Guid chatId, CancellationToken ct);
    Task UpdateAsync(SocialMember entity, CancellationToken ct);
    Task DeleteAsync(Guid id, CancellationToken ct);
}
