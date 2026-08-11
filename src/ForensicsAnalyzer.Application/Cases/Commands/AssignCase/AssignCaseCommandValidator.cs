using FluentValidation;

namespace ForensicsAnalyzer.Application.Cases.Commands.AssignCase;

public sealed class AssignCaseCommandValidator : AbstractValidator<AssignCaseCommand>
{
    public AssignCaseCommandValidator()
    {
        RuleFor(x => x.CaseId).NotEmpty();
        RuleFor(x => x.UserId).NotEmpty();
    }
}
