﻿using RecruitmentTask.Application.Abstraction.Clock;
using RecruitmentTask.Application.Abstraction.Messaging;
using RecruitmentTask.Domain.Abstractions;
using RecruitmentTask.Domain.People;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentTask.Application.People.CreatePerson;

internal sealed class CreatePersonCommandHandler : ICommandHandler<CreatePersonCommand, Guid>
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CreatePersonCommandHandler(
        IPersonRepository personRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<Guid>> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var utcNow = _dateTimeProvider.UtcNow;

        var personalData = new PersonalData(request.FirstName, request.LastName, request.BirthDate, request.PhoneNumber);
        var address = new Address(request.StreetName, request.HouseNumber, request.ApartmentNumber, request.Town, request.PostalCode);

        var dateOnlyUtc = DateOnly.FromDateTime(utcNow);

        var person = Person.CreateNew(utcNow, personalData, address, dateOnlyUtc);

        bool identicalDataPersonExists = await _personRepository.IdenticalDataPersonExistAsync(person);

        if (identicalDataPersonExists)
        {
            return Result.Failure<Guid>(PersonErrors.IdenticalData);
        }

        _personRepository.Add(person);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return person.Id;
    }
}
