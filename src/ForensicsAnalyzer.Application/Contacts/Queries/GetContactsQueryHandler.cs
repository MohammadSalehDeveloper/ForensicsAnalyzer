using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.Contacts;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.Contacts.Queries;

public sealed class GetContactsQueryHandler : IRequestHandler<GetContactsQuery, List<ContactDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetContactsQueryHandler(IApplicationDbContext context, IMapper mapper)
    { _context = context; _mapper = mapper; }

    public async Task<List<ContactDto>> Handle(GetContactsQuery request, CancellationToken ct)
    {
        var entities = await _context.Contacts.AsNoTracking()
            .Where(c => c.ArtifactId == request.ArtifactId).ToListAsync(ct);
        return _mapper.Map<List<ContactDto>>(entities);
    }
}
