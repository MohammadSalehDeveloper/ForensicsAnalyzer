using AutoMapper;
using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Contracts.CallLogs;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ForensicsAnalyzer.Application.CallLogs.Queries;

public sealed class GetCallLogByIdQueryHandler : IRequestHandler<GetCallLogByIdQuery, CallLogDto?>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCallLogByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CallLogDto?> Handle(GetCallLogByIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _context.CallLogs
            .AsNoTracking()
            .FirstOrDefaultAsync(c => c.Id == request.Id, cancellationToken);

        return entity is null ? null : _mapper.Map<CallLogDto>(entity);
    }
}
