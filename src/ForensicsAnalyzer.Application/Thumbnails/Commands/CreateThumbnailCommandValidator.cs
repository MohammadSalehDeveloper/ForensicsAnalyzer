using FluentValidation;

namespace ForensicsAnalyzer.Application.Thumbnails.Commands;

public sealed class CreateThumbnailCommandValidator : AbstractValidator<CreateThumbnailCommand>
{
    public CreateThumbnailCommandValidator()
    {
        RuleFor(x => x.FileCustomId).NotEmpty();
        RuleFor(x => x.ThumbnailPath).NotEmpty().MaximumLength(1000);
    }
}
