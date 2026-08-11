using ForensicsAnalyzer.Contracts.Social;
using MediatR;

namespace ForensicsAnalyzer.Application.Social.Queries;

public record GetSocialMessagesQuery(Guid ChatId) : IRequest<List<SocialMessageDto>>;
