using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Social;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Social.Queries;

public sealed class GetSocialMembersQueryHandler : IRequestHandler<GetSocialMembersQuery, List<SocialMemberDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSocialMembersQueryHandler(IApplicationDbContext context, IMapper mapper)
    { _context = context; _mapper = mapper; }

    public async Task<List<SocialMemberDto>> Handle(GetSocialMembersQuery request, CancellationToken ct)
    {
        var entities = await _context.SocialMembers.AsNoTracking()
            .Where(m => m.ChatId == request.ChatId).ToListAsync(ct);
        return _mapper.Map<List<SocialMemberDto>>(entities);
    }
}
