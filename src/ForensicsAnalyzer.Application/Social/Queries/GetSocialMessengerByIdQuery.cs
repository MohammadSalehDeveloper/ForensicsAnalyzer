using ForensicsAnalyzer.Contracts.Social;
using MediatR;

namespace ForensicsAnalyzer.Application.Social.Queries;

public record GetSocialMessengerByIdQuery(Guid Id) : IRequest<SocialMessengerDto?>;
