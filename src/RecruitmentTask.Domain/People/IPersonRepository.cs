using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentTask.Domain.People;

public interface IPersonRepository
{
    Task<Person> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken);

    void Add(Person person);

    void Remove(Person person);

    Task<bool> IdenticalDataPersonExistAsync(Person person, CancellationToken cancellationToken);
}
