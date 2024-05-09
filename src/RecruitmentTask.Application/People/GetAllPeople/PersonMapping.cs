using AutoMapper;
using RecruitmentTask.Domain.People;

namespace RecruitmentTask.Application.People.GetAllPeople;

public class PersonMapping : Profile
{
    public PersonMapping()
    {
        CreateMap<Person, PersonResponse>()
            .ForMember(personResponse => personResponse.Id, opt => opt.MapFrom(person => person.Id))
            .ForMember(personResponse => personResponse.FirstName, opt => opt.MapFrom(person => person.PersonalData.FirstName))
            .ForMember(personResponse => personResponse.LastName, opt => opt.MapFrom(person => person.PersonalData.LastName))
            .ForMember(personResponse => personResponse.BirthDate, opt => opt.MapFrom(person => person.PersonalData.BirthDateUtc))
            .ForMember(personResponse => personResponse.Age, opt => opt.MapFrom(person => person.PersonalData.Age))
            .ForMember(personResponse => personResponse.PhoneNumber, opt => opt.MapFrom(person => person.PersonalData.PhoneNumber))
            .ForMember(personResponse => personResponse.StreetName, opt => opt.MapFrom(person => person.Address.StreetName))
            .ForMember(personResponse => personResponse.HouseNumber, opt => opt.MapFrom(person => person.Address.HouseNumber))
            .ForMember(personResponse => personResponse.ApartmentNumber, opt => opt.MapFrom(person => person.Address.ApartmentNumber))
            .ForMember(personResponse => personResponse.Town, opt => opt.MapFrom(person => person.Address.Town))
            .ForMember(personResponse => personResponse.PostalCode, opt => opt.MapFrom(person => person.Address.PostalCode));
    }
}
