using AutoMapper;
using RecruitmentTask.Application.Abstraction.Messaging;
using RecruitmentTask.Application.People.GetAllPeople;
using RecruitmentTask.Domain.Abstractions;
using RecruitmentTask.Domain.People;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentTask.Application.People.GetPerson;

internal sealed class GetPersonQueryHandler : IQueryHandler<GetPersonQuery, PersonResponse>
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public GetPersonQueryHandler(
        IPersonRepository personRepository,
        IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<Result<PersonResponse>> Handle(GetPersonQuery request, CancellationToken cancellationToken)
    {
        var person = await _personRepository.GetByIdAsync(request.Id, cancellationToken);

        if (person is null)
        {
            return Result.Failure<PersonResponse>(PersonErrors.NotFound);
        }

        var personResponse = _mapper.Map<PersonResponse>(person);

        return personResponse;
    }
}
