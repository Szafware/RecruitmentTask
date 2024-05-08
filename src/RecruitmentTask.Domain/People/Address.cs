namespace RecruitmentTask.Domain.People;

public record Address(
    string StreetName,
    string HouseNumber,
    string ApartmentNumber,
    string Town,
    string PostalCode);
