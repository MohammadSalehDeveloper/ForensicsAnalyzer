using ForensicsAnalyzer.Contracts.Social;
using MediatR;

namespace ForensicsAnalyzer.Application.Social.Queries;

public record GetSocialMemberByIdQuery(Guid Id) : IRequest<SocialMemberDto?>;
