using RecruitmentTask.Application.Abstraction.Messaging;
using System.Collections.Generic;

namespace RecruitmentTask.Application.People.GetAllPeople;

public sealed record GetAllPeopleQuery : IQuery<IReadOnlyList<PersonResponse>>;
