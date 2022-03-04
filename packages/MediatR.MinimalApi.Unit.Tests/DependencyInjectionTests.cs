using System.Linq;
using MediatR.MinimalApi.GeneralPipelineBehavior;
using MediatR.MinimalApi.SpecificPipelineBehavior;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace MediatR.MinimalApi.Unit.Tests;

public class DependencyInjectionTests
{
    [Fact]
    public void Test_ConcreteBeforeOpenGeneric_ExpectToFail()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddTransient(
            typeof(IPipelineBehavior<GetMoneyRequest, GetMoneyResponse>), 
            typeof(GetMoneyBehavior));

        builder.Services.AddTransient(
            typeof(IPipelineBehavior<,>), 
            typeof(AllRequestsBehavior<,>));

        var app = builder.Build();

        var handlers = app.Services
            .GetServices<IPipelineBehavior<GetMoneyRequest, GetMoneyResponse>>()
            .ToList();

        Assert.True(2 == handlers.Count);

        var handlerTypes = handlers
            .Select(h => h.GetType())
            .ToList();

        Assert.Contains(handlerTypes, e => e.Equals(typeof(GetMoneyBehavior)));
        Assert.Contains(handlerTypes, e => e.Equals(typeof(AllRequestsBehavior<GetMoneyRequest,GetMoneyResponse>)));
        Assert.True(handlerTypes[0] != handlerTypes[1]);
    }

    [Fact]
    public void Test_RegisterOpenGenericBeforeConcrete_ExpectToSucceed()
    {
        var builder = WebApplication.CreateBuilder();

        builder.Services.AddTransient(
            typeof(IPipelineBehavior<,>), 
            typeof(AllRequestsBehavior<,>));

        builder.Services.AddTransient(
            typeof(IPipelineBehavior<GetMoneyRequest, GetMoneyResponse>), 
            typeof(GetMoneyBehavior));

        var app = builder.Build();

        var handlers = app.Services
            .GetServices<IPipelineBehavior<GetMoneyRequest, GetMoneyResponse>>()
            .ToList();

        Assert.True(2 == handlers.Count);

        var handlerTypes = handlers
            .Select(h => h.GetType())
            .ToList();

        Assert.Contains(handlerTypes, e => e.Equals(typeof(GetMoneyBehavior)));
        Assert.Contains(handlerTypes, e => e.Equals(typeof(AllRequestsBehavior<GetMoneyRequest,GetMoneyResponse>)));
        Assert.True(handlerTypes[0] != handlerTypes[1]);
    }
}