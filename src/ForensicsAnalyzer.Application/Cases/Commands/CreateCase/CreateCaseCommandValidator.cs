namespace ForensicsAnalyzer.Application.Cases.Commands.CreateCase;

using FluentValidation;

public class CreateCaseCommandValidator : AbstractValidator<CreateCaseCommand>
{
    public CreateCaseCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Case name is required")
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(2000);

        RuleFor(x => x.SourceId)
            .NotEmpty().WithMessage("SourceId is required for a case");
    }
}
