using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Social;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Social.Queries;

public sealed class GetSocialMemberByIdQueryHandler : IRequestHandler<GetSocialMemberByIdQuery, SocialMemberDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSocialMemberByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SocialMemberDto?> Handle(GetSocialMemberByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.SocialMembers
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        return entity is null ? null : _mapper.Map<SocialMemberDto>(entity);
    }
}
