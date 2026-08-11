using ForensicsAnalyzer.Contracts.Social;
using MediatR;

namespace ForensicsAnalyzer.Application.Social.Queries;

public record GetSocialChatsQuery(Guid MessengerId) : IRequest<List<SocialChatDto>>;
