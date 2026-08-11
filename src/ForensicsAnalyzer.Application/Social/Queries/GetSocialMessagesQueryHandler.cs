using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Social;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Social.Queries;

public sealed class GetSocialMessagesQueryHandler : IRequestHandler<GetSocialMessagesQuery, List<SocialMessageDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSocialMessagesQueryHandler(IApplicationDbContext context, IMapper mapper)
    { _context = context; _mapper = mapper; }

    public async Task<List<SocialMessageDto>> Handle(GetSocialMessagesQuery request, CancellationToken ct)
    {
        var entities = await _context.SocialMessages.AsNoTracking()
            .Where(m => m.ChatId == request.ChatId).ToListAsync(ct);
        return _mapper.Map<List<SocialMessageDto>>(entities);
    }
}
