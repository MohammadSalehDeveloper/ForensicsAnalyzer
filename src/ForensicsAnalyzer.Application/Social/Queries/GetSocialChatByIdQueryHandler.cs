using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Social;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Social.Queries;

public sealed class GetSocialChatByIdQueryHandler : IRequestHandler<GetSocialChatByIdQuery, SocialChatDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSocialChatByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SocialChatDto?> Handle(GetSocialChatByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.SocialChats
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        return entity is null ? null : _mapper.Map<SocialChatDto>(entity);
    }
}
