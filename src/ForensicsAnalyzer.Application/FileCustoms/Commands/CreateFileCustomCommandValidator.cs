using FluentValidation;

namespace ForensicsAnalyzer.Application.FileCustoms.Commands;

public sealed class CreateFileCustomCommandValidator : AbstractValidator<CreateFileCustomCommand>
{
    public CreateFileCustomCommandValidator()
    {
        RuleFor(x => x.FileName).NotEmpty().MaximumLength(255);
        RuleFor(x => x.Extension).NotEmpty().MaximumLength(20);
        RuleFor(x => x.FullPath).NotEmpty().MaximumLength(1000);
        RuleFor(x => x.Category).NotEmpty().MaximumLength(100);
    }
}
