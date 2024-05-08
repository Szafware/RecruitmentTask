using RecruitmentTask.Domain.People;

namespace RecruitmentTask.Infrastructure.Repositories;

internal sealed class PersonRepository : Repository<Person>, IPersonRepository
{
    public PersonRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {
    }
}
