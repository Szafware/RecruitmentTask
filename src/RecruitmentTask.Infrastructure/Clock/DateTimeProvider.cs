using RecruitmentTask.Application.Abstraction.Clock;
using System;

namespace RecruitmentTask.Infrastructure.Clock;

internal sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}
