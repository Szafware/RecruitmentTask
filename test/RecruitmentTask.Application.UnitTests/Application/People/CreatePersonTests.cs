using FluentAssertions;
using Moq;
using RecruitmentTask.Application.Abstraction.Clock;
using RecruitmentTask.Application.People.CreatePerson;
using RecruitmentTask.Domain.Abstractions;
using RecruitmentTask.Domain.People;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentTask.Application.UnitTests.Application.People;

public class CreatePersonTests
{
    private readonly CreatePersonCommand _createPersonCommand;

    private readonly Mock<IUnitOfWork> _unitOfWorkMock;
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;

    public CreatePersonTests()
    {
        _createPersonCommand = new CreatePersonCommand(
            "Alan",
            "Kay",
            new DateOnly(1940, 5, 17),
            "100200300",
            "DigitalStreet",
            "10A",
            10,
            "Springfield",
            "10-500");

        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
    }

    [Fact]
    public async Task Handle_Should_ReturnSuccessResult_WhenPersonDataIsUnique()
    {
        // Arrange
        var personRepositoryMock = new Mock<IPersonRepository>();

        personRepositoryMock.Setup(
            pr => pr.IdenticalDataPersonExistAsync(
                It.IsAny<Person>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var handler = new CreatePersonCommandHandler(
            personRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _dateTimeProviderMock.Object);

        // Act
        var result = await handler.Handle(_createPersonCommand, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeEmpty();
    }

    [Fact]
    public async Task Handle_Should_ReturnFailureResult_WhenPersonDataIsNotUnique()
    {
        // Arrange
        var personRepositoryMock = new Mock<IPersonRepository>();

        personRepositoryMock.Setup(
            pr => pr.IdenticalDataPersonExistAsync(
                It.IsAny<Person>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new CreatePersonCommandHandler(
            personRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _dateTimeProviderMock.Object);

        // Act
        var result = await handler.Handle(_createPersonCommand, default);

        // Assert
        result.IsFailure.Should().BeTrue();
        result.Error.Should().Be(PersonErrors.IdenticalData);
    }

    [Fact]
    public async Task Handle_Should_CallAddOnRepository_WhenPersonDataIsUnique()
    {
        // Arrange
        var personRepositoryMock = new Mock<IPersonRepository>();

        personRepositoryMock.Setup(
            pr => pr.IdenticalDataPersonExistAsync(
                It.IsAny<Person>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var handler = new CreatePersonCommandHandler(
            personRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _dateTimeProviderMock.Object);

        // Act
        var result = await handler.Handle(_createPersonCommand, default);

        // Assert
        personRepositoryMock.Verify(
            pr => pr.Add(It.Is<Person>(p => p.Id == result.Value)),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldNot_CallAddOnRepository_WhenPersonDataIsNotUnique()
    {
        // Arrange
        var personRepositoryMock = new Mock<IPersonRepository>();

        personRepositoryMock.Setup(
            pr => pr.IdenticalDataPersonExistAsync(
                It.IsAny<Person>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new CreatePersonCommandHandler(
            personRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _dateTimeProviderMock.Object);

        // Act
        var result = await handler.Handle(_createPersonCommand, default);

        // Assert
        personRepositoryMock.Verify(
            pr => pr.Add(It.IsAny<Person>()),
            Times.Never);
    }

    [Fact]
    public async Task Handle_Should_CallSaveChangesOnUnitOfWork_WhenPersonDataIsUnique()
    {
        // Arrange
        var personRepositoryMock = new Mock<IPersonRepository>();

        personRepositoryMock.Setup(
            pr => pr.IdenticalDataPersonExistAsync(
                It.IsAny<Person>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(false);

        var unitOfWorkMock = new Mock<IUnitOfWork>();
        var dateTimeProviderMock = new Mock<IDateTimeProvider>();

        var handler = new CreatePersonCommandHandler(
            personRepositoryMock.Object,
            unitOfWorkMock.Object,
            dateTimeProviderMock.Object);

        // Act
        var result = await handler.Handle(_createPersonCommand, default);

        // Assert
        unitOfWorkMock.Verify(
            uof => uof.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldNot_CallSaveChangesOnUnitOfWork_WhenPersonDataIsNotUnique()
    {
        // Arrange
        var personRepositoryMock = new Mock<IPersonRepository>();

        personRepositoryMock.Setup(
            pr => pr.IdenticalDataPersonExistAsync(
                It.IsAny<Person>(),
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        var handler = new CreatePersonCommandHandler(
            personRepositoryMock.Object,
            _unitOfWorkMock.Object,
            _dateTimeProviderMock.Object);

        // Act
        var result = await handler.Handle(_createPersonCommand, default);

        // Assert
        _unitOfWorkMock.Verify(
            uof => uof.SaveChangesAsync(It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
