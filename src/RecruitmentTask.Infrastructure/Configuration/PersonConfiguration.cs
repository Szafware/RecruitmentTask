using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RecruitmentTask.Domain.People;

namespace RecruitmentTask.Infrastructure.Configuration;

internal class UserConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.ToTable("People");

        builder.HasKey(person => person.Id);

        builder.ComplexProperty(person => person.PersonalData);
        builder.ComplexProperty(person => person.Address);
    }
}
