using System;

namespace RecruitmentTask.Domain.People;

public sealed record PersonalData
{
    private PersonalData()
    {
    }

    private PersonalData(string firstName, string lastName, DateOnly birthDateUtc, string phoneNumber, DateTime utcNow)
    {
        FirstName = firstName;
        LastName = lastName;
        BirthDateUtc = birthDateUtc;
        PhoneNumber = phoneNumber;
        Age = CalculateAgeInternal(utcNow);
    }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public DateOnly BirthDateUtc { get; init; }

    public string PhoneNumber { get; init; }

    public int Age { get; private set; }

    public void CalculateAge(DateTime utcNow)
    {
        Age = CalculateAgeInternal(utcNow);
    }

    public static PersonalData Create(string firstName, string lastName, DateOnly birthDateUtc, string phoneNumber, DateTime utcNow)
    {
        var personalData = new PersonalData(firstName, lastName, birthDateUtc, phoneNumber, utcNow);

        return personalData;
    }

    private int CalculateAgeInternal(DateTime utcNow)
    {
        var dateOnlyUtcNow = DateOnly.FromDateTime(utcNow);

        int age = utcNow.Year - BirthDateUtc.Year;

        if (BirthDateUtc > dateOnlyUtcNow.AddYears(-age))
        {
            age--;
        }

        return age;
    }
}
