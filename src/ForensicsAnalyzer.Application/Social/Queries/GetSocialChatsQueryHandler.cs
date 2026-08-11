using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Social;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Social.Queries;

public sealed class GetSocialChatsQueryHandler : IRequestHandler<GetSocialChatsQuery, List<SocialChatDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSocialChatsQueryHandler(IApplicationDbContext context, IMapper mapper)
    { _context = context; _mapper = mapper; }

    public async Task<List<SocialChatDto>> Handle(GetSocialChatsQuery request, CancellationToken ct)
    {
        var entities = await _context.SocialChats.AsNoTracking()
            .Where(c => c.MessengerId == request.MessengerId).ToListAsync(ct);
        return _mapper.Map<List<SocialChatDto>>(entities);
    }
}
