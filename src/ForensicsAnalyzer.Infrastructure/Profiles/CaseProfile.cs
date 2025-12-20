using AutoMapper;
using ForensicsAnalyzer.Contracts.Cases;
using ForensicsAnalyzer.Domain.Cases;

namespace ForensicsAnalyzer.Infrastructure.Profiles;

public class CaseProfile : Profile
{
    public CaseProfile()
    {
        CreateMap<Case, CaseDto>();
    }
}
