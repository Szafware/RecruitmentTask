using AutoMapper;
using FluentAssertions;
using Moq;
using RecruitmentTask.Application.Abstraction.Clock;
using RecruitmentTask.Application.People.GetAllPeople;
using RecruitmentTask.Domain.People;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentTask.Application.UnitTests.Application.People;

public class GetAllPeopleTests
{
    private readonly GetAllPeopleQuery _getAllPeopleQuery;

    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;

    public GetAllPeopleTests()
    {
        _getAllPeopleQuery = new GetAllPeopleQuery();

        _dateTimeProviderMock = new Mock<IDateTimeProvider>();

        _dateTimeProviderMock.Setup(
            dtp => dtp.UtcNow)
            .Returns(DateTime.UtcNow);
    }

    [Fact]
    public async Task Handler_Should_ReturnSuccessResult_WithCorrectPeopleCount()
    {
        // Arrange
        var personRepositoryMock = new Mock<IPersonRepository>();

        var people = Enumerable.Repeat(CreatePerson("Alan", "Kay", new DateOnly(1940, 5, 17), "100200300", "DigitalStreet", "10A", 10, "Springfield", "10-500", DateTime.UtcNow), 5);

        personRepositoryMock.Setup(
            pr => pr.GetAllAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(people);

        var mapperMock = new Mock<IMapper>();

        var peopleResponses = people.Select(CreatePersonResponse).ToList();

        mapperMock.Setup(
            m => m.Map<List<PersonResponse>>(people))
            .Returns(peopleResponses);

        var handler = new GetAllPeopleQueryHandler(
            personRepositoryMock.Object,
            _dateTimeProviderMock.Object,
            mapperMock.Object);

        // Act
        var result = await handler.Handle(_getAllPeopleQuery, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().HaveCount(people.Count());
    }

    [Fact]
    public async Task Handler_Should_CalculateAgeForAllPeople()
    {
        // Arrange
        var personRepositoryMock = new Mock<IPersonRepository>();

        var people = Enumerable.Repeat(CreatePerson("Alan", "Kay", new DateOnly(1940, 5, 17), "100200300", "DigitalStreet", "10A", 10, "Springfield", "10-500", DateTime.UtcNow), 5);

        personRepositoryMock.Setup(
            pr => pr.GetAllAsync(
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(people);

        var mapperMock = new Mock<IMapper>();

        var peopleResponses = people.Select(CreatePersonResponse).ToList();

        mapperMock.Setup(
            m => m.Map<List<PersonResponse>>(people))
            .Returns(peopleResponses);

        var handler = new GetAllPeopleQueryHandler(
            personRepositoryMock.Object,
            _dateTimeProviderMock.Object,
            mapperMock.Object);

        // Act
        var result = await handler.Handle(_getAllPeopleQuery, default);

        // Assert
        result.Value.Should().OnlyContain(pr => pr.Age != 0);
    }

    private Person CreatePerson(
        string firstName,
        string lastName,
        DateOnly birthDate,
        string phoneNumber,
        string streetName,
        string houseNumber,
        int? apartmentNumber,
        string town,
        string postalCode,
        DateTime utcNow)
    {
        var personalData = PersonalData.Create(firstName, lastName, birthDate, phoneNumber, utcNow);
        var address = new Address(streetName, houseNumber, apartmentNumber, town, postalCode);

        var person = Person.Create(utcNow, personalData, address);

        return person;
    }

    private PersonResponse CreatePersonResponse(Person person)
    {
        var personResponse = new PersonResponse
        {
            FirstName = person.PersonalData.FirstName,
            LastName = person.PersonalData.LastName,
            BirthDate = person.PersonalData.BirthDateUtc,
            Age = person.PersonalData.Age,
            PhoneNumber = person.PersonalData.PhoneNumber,
            StreetName = person.Address.StreetName,
            HouseNumber = person.Address.HouseNumber,
            ApartmentNumber = person.Address.ApartmentNumber,
            Town = person.Address.Town,
            PostalCode = person.Address.PostalCode
        };

        return personResponse;
    }
}
