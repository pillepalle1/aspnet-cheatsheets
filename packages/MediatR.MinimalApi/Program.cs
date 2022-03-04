// disable unreachable code warning for this file
#pragma warning disable 0162

using MediatR;
using MediatR.MinimalApi.Notification;
using MediatR.MinimalApi.SpecificPipelineBehavior;
using MediatR.MinimalApi.GeneralPipelineBehavior;
using MediatR.MinimalApi.RequestResponse;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddTransient(typeof(IPipelineBehavior<GetMoneyRequest, GetMoneyResponse>), typeof(GetMoneyBehavior));

// set true to add behavior to all requests
var activateGlobalBehaviorShowcase = true;
if (activateGlobalBehaviorShowcase)
{
    builder.Services.AddTransient(
        typeof(IPipelineBehavior<,>),
        typeof(AllRequestsBehavior<,>));
}

var app = builder.Build();

app.MapGet("/age", async (IMediator mediator) =>
{
    return await mediator.Send(new GetAgeRequest()
    {
        Name = "github.com/pillepalle1"
    });
});

app.MapGet("/notify", async (IMediator mediator, string? query) =>
{
    await mediator.Publish(new QueryNotification()
    {
        Query = query
    });

    return Results.Ok(query);
});

app.MapGet("/money", async (IMediator mediator) =>
{
    return await mediator.Send(new GetMoneyRequest()
    {
        Amount = 200.0d
    });
});

app.Run();
