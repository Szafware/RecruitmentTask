using RecruitmentTask.Application.Abstraction.Messaging;
using System;

namespace RecruitmentTask.Application.People.CreatePerson;

public record CreatePersonCommand(
    string FirstName,
    string LastName,
    DateOnly BirthDate,
    string PhoneNumber,
    string StreetName,
    string HouseNumber,
    int? ApartmentNumber,
    string Town,
    string PostalCode) : ICommand<Guid>;
