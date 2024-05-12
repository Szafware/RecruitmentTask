using FluentAssertions;
using RecruitmentTask.Domain.People;
using System;

namespace RecruitmentTask.Application.UnitTests.Domain;

public class PersonalDataTests
{
    private readonly DateTime _pointInTime;
    private readonly DateOnly _alansBirthDate;
    private readonly int _expectedAge = 83;

    public PersonalDataTests()
    {
        _pointInTime = new DateTime(2024, 05, 13);
        _alansBirthDate = new DateOnly(1940, 5, 17);
        _expectedAge = 83;
    }

    [Fact]
    public void Create_Should_CalculateAgeProperly()
    {
        // Act
        var personalData = PersonalData.Create("Alan", "Kay", _alansBirthDate, "100200300", _pointInTime);

        // Assert
        personalData.Age.Should().Be(_expectedAge);
    }

    [Fact]
    public void CalculateAge_Should_CalculateAgeProperly()
    {
        // Act
        var personalData = PersonalData.Create("Alan", "Kay", _alansBirthDate, "100200300", _pointInTime);
        personalData.CalculateAge(_pointInTime);

        // Assert
        personalData.Age.Should().Be(_expectedAge);
    }
}
