using AutoMapper;
using RecruitmentTask.Application.Abstraction.Clock;
using RecruitmentTask.Application.Abstraction.Messaging;
using RecruitmentTask.Domain.Abstractions;
using RecruitmentTask.Domain.People;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentTask.Application.People.GetAllPeople;

internal sealed class GetAllPeopleQueryHandler : IQueryHandler<GetAllPeopleQuery, IReadOnlyList<PersonResponse>>
{
    private readonly IPersonRepository _personRepository;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IMapper _mapper;

    public GetAllPeopleQueryHandler(
        IPersonRepository personRepository,
        IDateTimeProvider dateTimeProvider,
        IMapper mapper)
    {
        _personRepository = personRepository;
        _dateTimeProvider = dateTimeProvider;
        _mapper = mapper;
    }

    public async Task<Result<IReadOnlyList<PersonResponse>>> Handle(GetAllPeopleQuery request, CancellationToken cancellationToken)
    {
        var allPeople = await _personRepository.GetAllAsync(cancellationToken);

        var utcNow = _dateTimeProvider.UtcNow;

        foreach (var person in allPeople)
        {
            person.PersonalData.CalculateAge(utcNow);
        }

        var allPeopleResponse = _mapper.Map<List<PersonResponse>>(allPeople);

        return allPeopleResponse;
    }
}
