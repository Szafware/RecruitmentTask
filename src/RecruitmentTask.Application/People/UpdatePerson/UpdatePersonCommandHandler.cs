using RecruitmentTask.Application.Abstraction.Clock;
using RecruitmentTask.Application.Abstraction.Messaging;
using RecruitmentTask.Domain.Abstractions;
using RecruitmentTask.Domain.People;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentTask.Application.People.UpdatePerson;

internal sealed class UpdatePersonCommandHandler : ICommandHandler<UpdatePersonCommand>
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public UpdatePersonCommandHandler(
        IPersonRepository personRepository,
        IUnitOfWork unitOfWork,
        IDateTimeProvider dateTimeProvider)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (person is null)
        {
            return Result.Failure(PersonErrors.NotFound);
        }

        var utcNow = _dateTimeProvider.UtcNow;

        var personalData = PersonalData.Create(request.FirstName, request.LastName, request.BirthDate, request.PhoneNumber, utcNow);
        var address = new Address(request.StreetName, request.HouseNumber, request.ApartmentNumber, request.Town, request.PostalCode);

        person.SetAddress(address);
        person.SetPersonalData(personalData);

        bool identicalDataPersonExists = await _personRepository.IdenticalDataPersonExistAsync(person, cancellationToken);

        if (identicalDataPersonExists)
        {
            return Result.Failure<Guid>(PersonErrors.IdenticalData);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
