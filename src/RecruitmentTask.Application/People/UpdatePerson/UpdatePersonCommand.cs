using RecruitmentTask.Application.Abstraction.Messaging;
using System;

namespace RecruitmentTask.Application.People.UpdatePerson;

public record UpdatePersonCommand(
    Guid Id,
    string FirstName,
    string LastName,
    DateOnly BirthDate,
    string PhoneNumber,
    string StreetName,
    string HouseNumber,
    int? ApartmentNumber,
    string Town,
    string PostalCode) : ICommand;
