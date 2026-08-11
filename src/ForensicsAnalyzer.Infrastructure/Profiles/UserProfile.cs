using AutoMapper;
using ForensicsAnalyzer.Contracts.Users;
using ForensicsAnalyzer.Infrastructure.Persistence;

namespace ForensicsAnalyzer.Infrastructure.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<ApplicationUser, UserDto>();
    }
}
