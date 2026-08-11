using AutoMapper;
using ForensicsAnalyzer.Contracts.LocationImages;
using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Infrastructure.Profiles;

public class LocationImageProfile : Profile
{
    public LocationImageProfile()
    {
        CreateMap<LocationImage, LocationImageDto>();
    }
}
