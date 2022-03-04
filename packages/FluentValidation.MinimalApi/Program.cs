using System.Reflection;
using FluentValidation;
using FluentValidation.MinimalApi.Features.Validation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

var app = builder.Build();

app.MapGet("/money", async (
    IEnumerable<IValidator<GetMoneyRequest>> validators,
    string? name) =>
{
    var request = new GetMoneyRequest()
    {
        Name = name ?? String.Empty,
        Amount = 2000.0d * Random.Shared.NextDouble() - 500.0d
    };

    if (validators.Any())
    {
        var validationResults = await Task.WhenAll(validators
            .Select(v => v.ValidateAsync(request, CancellationToken.None))
            .ToList());

        var validationFailures = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();

        if (validationFailures.Any())
        {
            return Results.BadRequest(new { Errors = validationFailures.Select(x => x.ErrorMessage).ToList() });
        }
    }

    return Results.Ok(request);
});

app.Run();
