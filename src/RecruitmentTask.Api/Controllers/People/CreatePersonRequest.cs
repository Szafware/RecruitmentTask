using System;

namespace RecruitmentTask.Api.Controllers.People;

public sealed record CreatePersonRequest(
    string FirstName,
    string LastName,
    DateOnly BirthDate,
    string PhoneNumber,
    string StreetName,
    string HouseNumber,
    int? ApartmentNumber,
    string Town,
    string PostalCode);
