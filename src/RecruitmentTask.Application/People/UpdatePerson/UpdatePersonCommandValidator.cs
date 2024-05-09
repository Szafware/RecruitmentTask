using FluentValidation;
using RecruitmentTask.Application.People.UpdatePerson;
using System;

namespace RecruitmentTask.Application.People.CreatePerson;

public class UpdatePersonCommandValidator : AbstractValidator<UpdatePersonCommand>
{
    private readonly string _onlyLettersRegex = @"^[A-Za-z]+$";
    private readonly string _allPolishLettersRegex = @"^[a-zA-ZąĄćĆęĘńŃóÓżŻźŹ]+$";
    private readonly string _polishPostalCodeRegex = @"^[0-9]{2}-[0-9]{3}";
    private readonly string _onlyDigitsRegex = @"^\d+$";
    private readonly string _nonMixableDigitsAndLettersRegex = @"^([a-zA-Z]+(\d+)?|\d+[a-zA-Z]+)$";

    public UpdatePersonCommandValidator()
    {
        RuleFor(person => person.Id)
            .NotEmpty();

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
            .Length(9)
            .Matches(_onlyDigitsRegex)
            .WithMessage("Digits only eg. \"123456789\".");

        RuleFor(person => person.StreetName)
            .NotEmpty()
            .Length(2, 20)
            .Matches(_allPolishLettersRegex);

        RuleFor(person => person.HouseNumber)
            .NotEmpty()
            .Length(3, 6)
            .Matches(_nonMixableDigitsAndLettersRegex);

        RuleFor(person => person.ApartmentNumber)
            .InclusiveBetween(1, 1000)
            .When(p => p is not null);

        RuleFor(person => person.Town)
            .NotEmpty()
            .Length(2, 20)
            .Matches(_allPolishLettersRegex);

        RuleFor(person => person.PostalCode)
            .NotEmpty()
            .Matches(_polishPostalCodeRegex);
    }
}
