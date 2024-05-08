using Microsoft.EntityFrameworkCore;
using RecruitmentTask.Domain.Abstractions;
using RecruitmentTask.Domain.People;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace RecruitmentTask.Infrastructure.Repositories;

internal abstract class Repository<TEntity> 
    where TEntity : Entity
{
    protected readonly ApplicationDbContext _applicationDbContext;

    protected Repository(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Person> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var person = await _applicationDbContext.Set<Person>().FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);

        return person;
    }

    public async Task<IEnumerable<Person>> GetAllAsync(CancellationToken cancellationToken)
    {
        var people = await _applicationDbContext.Set<Person>().ToListAsync(cancellationToken);

        return people;
    }

    public void Add(TEntity entity)
    {
        _applicationDbContext.Add(entity);
    }

    public void Remove(TEntity entity)
    {
        _applicationDbContext.Remove(entity);
    }
}
