using System;

namespace RecruitmentTask.Domain.People;

public record PersonalData(
    string FirstName,
    string LastName,
    DateTime BirthDateUtc,
    string PhoneNumber)
{
    public int Age
    {
        get
        {
            DateTime nowUtc = DateTime.UtcNow;
            int age = nowUtc.Year - BirthDateUtc.Year;

            if (nowUtc.Month < BirthDateUtc.Month || (nowUtc.Month == BirthDateUtc.Month && nowUtc.Day < BirthDateUtc.Day))
            {
                age--;
            }

            return age;
        }
    }
}
