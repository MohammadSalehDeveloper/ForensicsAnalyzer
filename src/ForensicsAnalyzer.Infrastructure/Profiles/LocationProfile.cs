using AutoMapper;
using ForensicsAnalyzer.Contracts.Locations;
using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Infrastructure.Profiles;

public class LocationProfile : Profile
{
    public LocationProfile()
    {
        CreateMap<Location, LocationDto>();
    }
}
