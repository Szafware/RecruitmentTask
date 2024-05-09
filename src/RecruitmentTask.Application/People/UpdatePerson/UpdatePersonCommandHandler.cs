using RecruitmentTask.Application.Abstraction.Messaging;
using RecruitmentTask.Domain.Abstractions;
using RecruitmentTask.Domain.People;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentTask.Application.People.UpdatePerson;

internal sealed class UpdatePersonCommandHandler : ICommandHandler<UpdatePersonCommand>
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePersonCommandHandler(
        IPersonRepository personRepository,
        IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id, cancellationToken);
        
        if (person is null)
        {
            return Result.Failure(PersonErrors.NotFound);
        }

        var personalData = new PersonalData(request.FirstName, request.LastName, request.BirthDate, request.PhoneNumber);
        var address = new Address(request.StreetName, request.HouseNumber, request.ApartmentNumber, request.Town, request.PostalCode);

        person.SetAddress(address);
        person.SetPersonalData(personalData);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
