using System;

namespace RecruitmentTask.Domain.People;

public record PersonalData(
    string FirstName,
    string LastName,
    DateOnly BirthDateUtc,
    string PhoneNumber)
{
    public int Age { get; private set; }

    public void SetAge(DateOnly utcNow)
    {
        int age = utcNow.Year - BirthDateUtc.Year;

        if (utcNow.Month < BirthDateUtc.Month || (utcNow.Month == BirthDateUtc.Month && utcNow.Day < BirthDateUtc.Day))
        {
            age--;
        }

        Age = age;
    }
}
