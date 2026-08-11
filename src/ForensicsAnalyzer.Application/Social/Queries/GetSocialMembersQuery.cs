using ForensicsAnalyzer.Contracts.Social;
using MediatR;

namespace ForensicsAnalyzer.Application.Social.Queries;

public record GetSocialMembersQuery(Guid ChatId) : IRequest<List<SocialMemberDto>>;
