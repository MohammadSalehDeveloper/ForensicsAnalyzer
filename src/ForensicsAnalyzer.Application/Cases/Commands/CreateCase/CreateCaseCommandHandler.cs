using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Cases;
using MediatR;

namespace ForensicsAnalyzer.Application.Cases.Commands.CreateCase;

public class CreateCaseCommandHandler 
    : IRequestHandler<CreateCaseCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateCaseCommandHandler(
        IApplicationDbContext context,
        ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<Guid> Handle(
        CreateCaseCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new Case(
            request.Name,
            request.Description,
            _currentUser.UserId
        );

        _context.Cases.Add(entity);
        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
