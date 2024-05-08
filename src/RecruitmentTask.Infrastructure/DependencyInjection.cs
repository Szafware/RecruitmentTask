using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecruitmentTask.Application.Abstraction.Clock;
using RecruitmentTask.Domain.Abstractions;
using RecruitmentTask.Domain.People;
using RecruitmentTask.Infrastructure.Clock;
using RecruitmentTask.Infrastructure.Repositories;
using System;

namespace RecruitmentTask.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("LocalDatabase")
            ?? throw new ArgumentNullException(nameof(configuration));

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(connectionString);
        });


        services.AddScoped<IPersonRepository, PersonRepository>();

        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<ApplicationDbContext>());

        services.AddTransient<IDateTimeProvider, DateTimeProvider>();

        return services;
    }
}
