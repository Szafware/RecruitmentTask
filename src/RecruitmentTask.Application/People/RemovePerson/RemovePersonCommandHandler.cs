using RecruitmentTask.Application.Abstraction.Messaging;
using RecruitmentTask.Domain.Abstractions;
using RecruitmentTask.Domain.People;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentTask.Application.People.RemovePerson;

internal sealed class RemovePersonCommandHandler : ICommandHandler<RemovePersonCommand>
{
    private readonly IPersonRepository _personRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemovePersonCommandHandler(
        IPersonRepository personRepository,
        IUnitOfWork unitOfWork)
    {
        _personRepository = personRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(RemovePersonCommand request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.PersonId, cancellationToken);

        if (person is null)
        {
            return Result.Failure<Guid>(PersonErrors.NotFound);
        }

        _personRepository.Remove(person);

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
