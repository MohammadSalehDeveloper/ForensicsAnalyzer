using AutoMapper;
using ForensicsAnalyzer.Contracts.Cases;
using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Infrastructure.Profiles;

public class CaseProfile : Profile
{
    public CaseProfile()
    {
        CreateMap<Case, CaseDto>()
            .ForMember(dest => dest.OwnerId, opt => opt.MapFrom(src => src.UserId));
    }
}
