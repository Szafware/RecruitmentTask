using RecruitmentTask.Application.Abstraction.Messaging;
using System;

namespace RecruitmentTask.Application.People.RemovePerson;

public record RemovePersonCommand(Guid PersonId) : ICommand;
