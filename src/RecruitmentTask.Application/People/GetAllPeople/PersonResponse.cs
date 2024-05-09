using System;

namespace RecruitmentTask.Application.People.GetAllPeople;

public sealed class PersonResponse
{
    public Guid Id { get; init; }

    public string FirstName { get; init; }

    public string LastName { get; init; }

    public DateOnly BirthDate { get; init; }

    public string PhoneNumber { get; init; }

    public int Age { get; init; }

    public string StreetName { get; init; }

    public string HouseNumber { get; init; }

    public int? ApartmentNumber { get; init; }

    public string Town { get; init; }

    public string PostalCode { get; init; }
}
