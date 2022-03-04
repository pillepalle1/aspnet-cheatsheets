namespace FluentValidation.MinimalApi.Features.Validation;
public class GetMoneyRequestAmountValidator : AbstractValidator<GetMoneyRequest>
{
    public GetMoneyRequestAmountValidator()
    {
        RuleFor(x => x.Amount)
            .GreaterThanOrEqualTo(0.0d).WithMessage("{PropertyName} must be a positive number");
    }
}