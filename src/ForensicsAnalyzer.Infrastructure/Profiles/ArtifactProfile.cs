using AutoMapper;
using ForensicsAnalyzer.Contracts.Artifacts;
using ForensicsAnalyzer.Domain.Entities;

namespace ForensicsAnalyzer.Infrastructure.Profiles;

public class ArtifactProfile : Profile
{
    public ArtifactProfile()
    {
        CreateMap<Artifact, ArtifactDto>();
        CreateMap<ArtifactItem, ArtifactItemDto>();
    }
}
