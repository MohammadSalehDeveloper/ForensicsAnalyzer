using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Social;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Social.Queries;

public sealed class GetSocialMessengerByIdQueryHandler : IRequestHandler<GetSocialMessengerByIdQuery, SocialMessengerDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetSocialMessengerByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<SocialMessengerDto?> Handle(GetSocialMessengerByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.SocialMessengers
            .AsNoTracking()
            .FirstOrDefaultAsync(m => m.Id == request.Id, cancellationToken);

        return entity is null ? null : _mapper.Map<SocialMessengerDto>(entity);
    }
}
