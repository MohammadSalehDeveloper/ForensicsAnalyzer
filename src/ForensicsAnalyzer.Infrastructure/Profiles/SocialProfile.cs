using AutoMapper;
using ForensicsAnalyzer.Contracts.Social;
using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Infrastructure.Profiles;

public class SocialProfile : Profile
{
    public SocialProfile()
    {
        CreateMap<SocialMessenger, SocialMessengerDto>();
        CreateMap<SocialChat, SocialChatDto>();
        CreateMap<SocialMessage, SocialMessageDto>();
        CreateMap<SocialMember, SocialMemberDto>();
    }
}
