using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RecruitmentTask.Application.Abstraction.Behaviors;

namespace RecruitmentTask.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var thisAssembly = typeof(DependencyInjection).Assembly;

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(thisAssembly);

            configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            configuration.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });

        services.AddValidatorsFromAssembly(thisAssembly);

        services.AddAutoMapper(thisAssembly);

        return services;
    }
}
