using AutoMapper;
using ForensicsAnalyzer.Contracts.CallLogs;
using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Infrastructure.Profiles;

public class CallLogProfile : Profile
{
    public CallLogProfile()
    {
        CreateMap<CallLog, CallLogDto>();
    }
}
