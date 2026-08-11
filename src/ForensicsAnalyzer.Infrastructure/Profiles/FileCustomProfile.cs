using AutoMapper;
using ForensicsAnalyzer.Contracts.Files;
using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Infrastructure.Profiles;

public class FileCustomProfile : Profile
{
    public FileCustomProfile()
    {
        CreateMap<FileCustom, FileCustomDto>()
            .ForMember(dest => dest.FilePermissions, opt => opt.MapFrom(src => src.Permissions));
    }
}
