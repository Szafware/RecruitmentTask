using System;

namespace RecruitmentTask.Domain.People;

public record PersonalData(
    string FirstName,
    string LastName,
    DateOnly BirthDateUtc,
    string PhoneNumber)
{
    public int Age { get; private set; }

    public void SetAge(DateOnly dateOnlyUtc)
    {
        int age = dateOnlyUtc.Year - BirthDateUtc.Year;

        if (dateOnlyUtc.Month < BirthDateUtc.Month || (dateOnlyUtc.Month == BirthDateUtc.Month && dateOnlyUtc.Day < BirthDateUtc.Day))
        {
            age--;
        }

        Age = age;
    }
}
