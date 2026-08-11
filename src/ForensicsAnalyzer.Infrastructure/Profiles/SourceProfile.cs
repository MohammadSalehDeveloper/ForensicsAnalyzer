using AutoMapper;
using ForensicsAnalyzer.Contracts.Sources;
using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Infrastructure.Profiles;

public class SourceProfile : Profile
{
    public SourceProfile()
    {
        CreateMap<Source, SourceDto>();
    }
}
