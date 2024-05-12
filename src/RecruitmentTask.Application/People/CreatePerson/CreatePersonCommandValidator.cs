using FluentValidation;
using System;

namespace RecruitmentTask.Application.People.CreatePerson;

internal class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    private readonly string _onlyLettersRegex = @"^[A-Za-z]+$";
    private readonly string _allPolishLettersRegex = @"^[a-zA-ZąĄćĆęĘńŃóÓżŻźŹ]+$";
    private readonly string _polishPostalCodeRegex = @"^[0-9]{2}-[0-9]{3}";
    private readonly string _onlyNineNumbersRegex = @"^\d{9}$";
    private readonly string _digitsAndLettersRegex = @"^[a-zA-Z0-9]+$";

    public CreatePersonCommandValidator()
    {
        RuleFor(person => person.FirstName)
            .NotEmpty()
            .Length(3, 15)
            .Matches(_onlyLettersRegex);

        RuleFor(person => person.LastName)
            .NotEmpty()
            .Length(2, 20)
            .Matches(_allPolishLettersRegex);

        RuleFor(person => person.BirthDate)
            .GreaterThan(DateOnly.FromDateTime(DateTime.Now.AddYears(-100)))
            .WithMessage("You're not that old, are ya?");

        RuleFor(person => person.BirthDate)
            .LessThan(DateOnly.FromDateTime(DateTime.Now.AddYears(-3)))
            .WithMessage("Sorry, no toddlers accepted.");

        RuleFor(person => person.PhoneNumber)
            .NotEmpty()
            .Matches(_onlyNineNumbersRegex)
            .WithMessage("Digits only eg. \"123456789\".");

        RuleFor(person => person.StreetName)
            .NotEmpty()
            .Length(2, 20)
            .Matches(_allPolishLettersRegex);

        RuleFor(person => person.HouseNumber)
            .NotEmpty()
            .Length(1, 6)
            .Matches(_digitsAndLettersRegex);

        RuleFor(person => person.ApartmentNumber)
            .InclusiveBetween(1, 1000)
            .When(p => p is not null);

        RuleFor(person => person.Town)
            .NotEmpty()
            .Length(2, 20)
            .Matches(_allPolishLettersRegex);

        RuleFor(person => person.PostalCode)
            .NotEmpty()
            .Matches(_polishPostalCodeRegex)
            .WithMessage("Enter postal code in format XX-XXX.");
    }
}
