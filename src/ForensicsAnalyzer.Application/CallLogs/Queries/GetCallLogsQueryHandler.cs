using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.CallLogs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.CallLogs.Queries;

public sealed class GetCallLogsQueryHandler : IRequestHandler<GetCallLogsQuery, List<CallLogDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCallLogsQueryHandler(IApplicationDbContext context, IMapper mapper)
    { _context = context; _mapper = mapper; }

    public async Task<List<CallLogDto>> Handle(GetCallLogsQuery request, CancellationToken ct)
    {
        var entities = await _context.CallLogs.AsNoTracking()
            .Where(c => c.ArtifactId == request.ArtifactId).ToListAsync(ct);
        return _mapper.Map<List<CallLogDto>>(entities);
    }
}
