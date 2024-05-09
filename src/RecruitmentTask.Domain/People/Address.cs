namespace RecruitmentTask.Domain.People;

public record Address(
    string StreetName,
    string HouseNumber,
    int? ApartmentNumber,
    string Town,
    string PostalCode);
