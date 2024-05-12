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

        builder.ComplexProperty(
            person => person.PersonalData,
            personalData =>
            {
                personalData.IsRequired();
                personalData.Property(personalData => personalData.FirstName).IsRequired().HasMaxLength(15);
                personalData.Property(personalData => personalData.LastName).IsRequired().HasMaxLength(20);
                personalData.Property(personalData => personalData.BirthDateUtc);
                personalData.Ignore(personalData => personalData.Age);
                personalData.Property(personalData => personalData.PhoneNumber).IsRequired().HasMaxLength(9);
            });

        builder.ComplexProperty(
            person => person.Address,
            address =>
            {
                address.IsRequired();
                address.Property(address => address.StreetName).IsRequired().HasMaxLength(20);
                address.Property(address => address.HouseNumber).IsRequired().HasMaxLength(6);
                address.Property(address => address.ApartmentNumber);
                address.Property(address => address.Town).IsRequired().HasMaxLength(20);
                address.Property(address => address.PostalCode).IsRequired().HasMaxLength(6);
            });
    }
}
