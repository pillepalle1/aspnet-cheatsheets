namespace FluentValidation.MinimalApi.Features.Validation;
public class GetMoneyRequestGeneralValidator : AbstractValidator<GetMoneyRequest>
{
    public GetMoneyRequestGeneralValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("I need to know who you are");

        RuleFor(x => x.Amount)
            .MustAsync(BeValidAmount).WithMessage("{PropertyName} is not a valid number");
    }

    private Task<bool> BeValidAmount(double amount, CancellationToken cancellationToken)
    {
        if (Double.IsNaN(amount))
        {
            return Task.FromResult(false);
        }

        if (Double.IsInfinity(amount))
        {
            return Task.FromResult(false);
        }

        return Task.FromResult(true);
    }
}