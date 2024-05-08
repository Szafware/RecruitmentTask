using System;

namespace RecruitmentTask.Domain.Abstractions;

public abstract class Entity
{
    protected Entity()
    {
    }
    
    protected Entity(
        Guid id,
        DateTime createdOnUtc)
    {
        Id = id;
        CreatedOnUtc = createdOnUtc;
    }

    public Guid Id { get; init; }

    public DateTime CreatedOnUtc { get; init; }
}
