using RecruitmentTask.Domain.Abstractions;
using System;

namespace RecruitmentTask.Domain.People;

public class Person : Entity
{
    public PersonalData PersonalData { get; private set; }

    public Address Address { get; private set; }

    private Person(
        Guid id,
        DateTime createdOnUtc)
        : base(id, createdOnUtc)
    {
        
    }

    public static Person CreateNew(DateTime createdOnUtc)
    {
        var person = new Person(Guid.NewGuid(), createdOnUtc);

        return person;
    }
}
