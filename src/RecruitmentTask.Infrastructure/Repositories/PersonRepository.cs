using Microsoft.EntityFrameworkCore;
using RecruitmentTask.Domain.People;
using System.Threading;
using System.Threading.Tasks;

namespace RecruitmentTask.Infrastructure.Repositories;

internal sealed class PersonRepository : Repository<Person>, IPersonRepository
{
    public PersonRepository(ApplicationDbContext applicationDbContext)
        : base(applicationDbContext)
    {
    }

    public async Task<bool> IdenticalDataPersonExistAsync(Person person, CancellationToken cancellationToken = default)
    {
        bool identicalPersonExists = await _applicationDbContext.Set<Person>().AnyAsync(p => p.Id != person.Id &&
                                                                                             p.PersonalData.FirstName == person.PersonalData.FirstName &&
                                                                                             p.PersonalData.LastName == person.PersonalData.LastName &&
                                                                                             p.PersonalData.BirthDateUtc == person.PersonalData.BirthDateUtc &&
                                                                                             p.PersonalData.PhoneNumber == person.PersonalData.PhoneNumber &&
                                                                                             p.Address.StreetName == person.Address.StreetName &&
                                                                                             p.Address.HouseNumber == person.Address.HouseNumber &&
                                                                                             p.Address.ApartmentNumber == person.Address.ApartmentNumber &&
                                                                                             p.Address.PostalCode == person.Address.PostalCode &&
                                                                                             p.Address.Town == person.Address.Town, cancellationToken);

        return identicalPersonExists;
    }
}
