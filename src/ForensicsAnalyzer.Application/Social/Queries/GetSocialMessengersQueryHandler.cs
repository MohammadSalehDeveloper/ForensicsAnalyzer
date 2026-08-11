using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Social;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Social.Queries;

public sealed class GetSocialMessengersQueryHandler : IRequestHandler<GetSocialMessengersQuery, List<SocialMessengerDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSocialMessengersQueryHandler(IApplicationDbContext context, IMapper mapper)
    { _context = context; _mapper = mapper; }

    public async Task<List<SocialMessengerDto>> Handle(GetSocialMessengersQuery request, CancellationToken ct)
    {
        var entities = await _context.SocialMessengers.AsNoTracking().ToListAsync(ct);
        return _mapper.Map<List<SocialMessengerDto>>(entities);
    }
}
