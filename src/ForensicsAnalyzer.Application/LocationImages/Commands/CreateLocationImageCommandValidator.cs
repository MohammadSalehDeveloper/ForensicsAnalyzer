using FluentValidation;

namespace ForensicsAnalyzer.Application.LocationImages.Commands;

public sealed class CreateLocationImageCommandValidator : AbstractValidator<CreateLocationImageCommand>
{
    public CreateLocationImageCommandValidator()
    {
        RuleFor(x => x.FileCustomId).NotEmpty();
    }
}
