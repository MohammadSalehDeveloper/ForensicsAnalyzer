using FluentValidation;

namespace ForensicsAnalyzer.Application.ArtifactItems.Commands;

public sealed class CreateArtifactItemCommandValidator : AbstractValidator<CreateArtifactItemCommand>
{
    public CreateArtifactItemCommandValidator()
    {
        RuleFor(x => x.ArtifactId).NotEmpty();
        RuleFor(x => x.ReferenceId).NotEmpty();
    }
}
