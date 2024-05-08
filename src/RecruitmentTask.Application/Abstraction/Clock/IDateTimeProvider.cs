using System;

namespace RecruitmentTask.Application.Abstraction.Clock;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}
