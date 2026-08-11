using AutoMapper;
using ForensicsAnalyzer.Contracts.Thumbnails;
using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Infrastructure.Profiles;

public class ThumbnailProfile : Profile
{
    public ThumbnailProfile()
    {
        CreateMap<Thumbnail, ThumbnailDto>();
    }
}
