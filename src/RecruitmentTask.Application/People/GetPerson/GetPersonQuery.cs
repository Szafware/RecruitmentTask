using RecruitmentTask.Application.Abstraction.Messaging;
using RecruitmentTask.Application.People.GetAllPeople;
using System;

namespace RecruitmentTask.Application.People.GetPerson;

public sealed record GetPersonQuery(Guid Id) : IQuery<PersonResponse>;
