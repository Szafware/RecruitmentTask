using Microsoft.AspNetCore.Builder;
using RecruitmentTask.Api.Middleware;

namespace RecruitmentTask.Api.Extensions;

public static class ApplicationBuilderExtensions
{
    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }
}
