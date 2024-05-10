using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;
using System;

namespace RecruitmentTask.Ui.Configuration;

internal sealed class TypeResolver : ITypeResolver
{
    private readonly IServiceProvider _serviceProvider;

    public TypeResolver(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
    }

    public object Resolve(Type type)
    {
        var service = _serviceProvider.GetRequiredService(type);

        return service;
    }
}
