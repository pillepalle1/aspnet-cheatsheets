namespace FluentValidation.MinimalApi.Features.Validation;
public class GetMoneyRequest
{
    public string Name { get; set; } = String.Empty;
    public double Amount { get; set; } = Double.NaN;
}