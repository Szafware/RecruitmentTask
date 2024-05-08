using RecruitmentTask.Application.Abstraction.Messaging;
using System;

namespace RecruitmentTask.Application.People.CreatePerson;

// TODO: Fill up Person properties
public record CreatePersonCommand(
    string FirstName,
    string LastName) : ICommand<Guid>;