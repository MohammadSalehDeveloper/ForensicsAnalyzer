using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Social;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Social.Queries;

public sealed class GetSocialMessageByIdQueryHandler : IRequestHandler<GetSocialMessageByIdQuery, SocialMessageDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSocialMessageByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SocialMessageDto?> Handle(GetSocialMessageByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.SocialMessages
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        return entity is null ? null : _mapper.Map<SocialMessageDto>(entity);
    }
}
