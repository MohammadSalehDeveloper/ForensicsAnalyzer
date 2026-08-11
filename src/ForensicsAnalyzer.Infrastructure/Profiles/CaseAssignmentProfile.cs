using AutoMapper;
using ForensicsAnalyzer.Contracts.Cases;
using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Infrastructure.Profiles;

public class CaseAssignmentProfile : Profile
{
    public CaseAssignmentProfile()
    {
        CreateMap<CaseAssignment, CaseAssignmentDto>();
    }
}
