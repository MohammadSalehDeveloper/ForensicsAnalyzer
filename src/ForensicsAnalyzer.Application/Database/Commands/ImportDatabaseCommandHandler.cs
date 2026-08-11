using ForensicsAnalyzer.Application.Interfaces;
using ForensicsAnalyzer.Domain.Entities;
using MediatR;

namespace ForensicsAnalyzer.Application.Database.Commands;

public sealed class ImportDatabaseCommandHandler : IRequestHandler<ImportDatabaseCommand>
{
    private readonly IApplicationDbContext _context;
    private readonly IIdentityService _identityService;

    public ImportDatabaseCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    {
        _context = context;
        _identityService = identityService;
    }

    public async Task Handle(ImportDatabaseCommand request, CancellationToken cancellationToken)
    {
        foreach (var userDto in request.Data.Users)
        {
            var existing = await _identityService.FindUserByEmailAsync(userDto.Email, cancellationToken);
            if (existing.Succeeded)
                continue;

            var result = await _identityService.CreateUserAsync(
                userDto.Email,
                userDto.Password,
                userDto.FullName ?? userDto.UserName,
                cancellationToken);

            if (!result.Succeeded)
                throw new InvalidOperationException(string.Join("; ", result.Errors));
        }

        foreach (var caseDto in request.Data.Cases)
        {
            var owner = await _identityService.FindUserByEmailAsync(caseDto.OwnerEmail, cancellationToken);
            if (!owner.Succeeded || owner.UserId is null)
                continue;

            _context.Cases.Add(new Case
            {
                Name = caseDto.Name,
                Description = caseDto.Description,
                SourceId = caseDto.SourceId,
                UserId = owner.UserId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            });
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
