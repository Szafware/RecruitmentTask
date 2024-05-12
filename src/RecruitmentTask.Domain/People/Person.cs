using RecruitmentTask.Domain.Abstractions;
using System;

namespace RecruitmentTask.Domain.People;

public sealed class Person : Entity
{
    private Person()
    {
    }

    private Person(
        Guid id,
        DateTime createdOnUtc,
        PersonalData personalData,
        Address address)
        : base(id, createdOnUtc)
    {
        PersonalData = personalData;
        Address = address;
    }

    public PersonalData PersonalData { get; private set; }

    public Address Address { get; private set; }

    public static Person CreateNew(DateTime createdOnUtc, PersonalData personalData, Address address)
    {
        var person = new Person(Guid.NewGuid(), createdOnUtc, personalData, address);

        return person;
    }

    public void SetAddress(Address address)
    {
        Address = address;
    }

    public void SetPersonalData(PersonalData personalData)
    {
        PersonalData = personalData;
    }
}
