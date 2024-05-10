using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;
using System;

namespace RecruitmentTask.Ui.Configuration;

internal sealed class TypeRegistrar : ITypeRegistrar
{
    private readonly IServiceCollection _serviceCollection;

    public TypeRegistrar(IServiceCollection serviceCollection)
    {
        _serviceCollection = serviceCollection;
    }

    public ITypeResolver Build()
    {
        var serviceProvider = _serviceCollection.BuildServiceProvider();

        var typeResolver = new TypeResolver(serviceProvider);

        return typeResolver;
    }

    public void Register(Type service, Type implementation)
    {
        _serviceCollection.AddSingleton(service, implementation);
    }

    public void RegisterInstance(Type service, object implementation)
    {
        _serviceCollection.AddSingleton(service, implementation);
    }

    public void RegisterLazy(Type service, Func<object> factory)
    {
    }
}
