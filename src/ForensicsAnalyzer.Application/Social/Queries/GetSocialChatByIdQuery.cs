using ForensicsAnalyzer.Contracts.Social;
using MediatR;

namespace ForensicsAnalyzer.Application.Social.Queries;

public record GetSocialChatByIdQuery(Guid Id) : IRequest<SocialChatDto?>;
