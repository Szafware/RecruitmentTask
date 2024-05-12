using FluentValidation;
using RecruitmentTask.Application.Constants;
using System;

namespace RecruitmentTask.Application.People.CreatePerson;

internal class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(person => person.FirstName)
            .NotEmpty()
            .Length(ValidationValueConstants.FirstNameCharacterCountMin, ValidationValueConstants.FirstNameCharacterCountMax)
            .Matches(ValidationRegexConstants.OnlyLettersRegex);

        RuleFor(person => person.LastName)
            .NotEmpty()
            .Length(ValidationValueConstants.LastNameCharacterCountMin, ValidationValueConstants.LastNameCharacterCountMax)
            .Matches(ValidationRegexConstants.AllPolishLettersRegex);

        RuleFor(person => person.BirthDate)
            .LessThan(DateOnly.FromDateTime(DateTime.Now.AddYears(-ValidationValueConstants.BirthDateYearMin)))
            .WithMessage(ValidationMessageConstants.BirthDateTooLowValidationMessage);

        RuleFor(person => person.BirthDate)
            .GreaterThan(DateOnly.FromDateTime(DateTime.Now.AddYears(-ValidationValueConstants.BirthDateYearMax)))
            .WithMessage(ValidationMessageConstants.BirthDateTooHighValidationMessage);

        RuleFor(person => person.PhoneNumber)
            .NotEmpty()
            .Length(ValidationValueConstants.PhoneNumberCharacterCount)
            .Matches(ValidationRegexConstants.OnlyDigitsRegex)
            .WithMessage(ValidationMessageConstants.PhoneNumberValidationMessage);

        RuleFor(person => person.StreetName)
            .NotEmpty()
            .Length(ValidationValueConstants.StreetNameCharacterCountMin, ValidationValueConstants.StreetNameCharacterCountMax)
            .Matches(ValidationRegexConstants.AllPolishLettersRegex);

        RuleFor(person => person.HouseNumber)
            .NotEmpty()
            .Length(ValidationValueConstants.HouseNumberCharacterCountMin, ValidationValueConstants.HouseNumberCharacterCountMax)
            .Matches(ValidationRegexConstants.DigitsAndLettersRegex);

        RuleFor(person => person.ApartmentNumber)
            .InclusiveBetween(ValidationValueConstants.ApartmentNumberValueMin, ValidationValueConstants.ApartmentNumberValueMax)
            .When(command => command.ApartmentNumber is not null);

        RuleFor(person => person.Town)
            .NotEmpty()
            .Length(ValidationValueConstants.TownCharacterCountMin, ValidationValueConstants.TownCharacterCountMax)
            .Matches(ValidationRegexConstants.AllPolishLettersRegex);

        RuleFor(person => person.PostalCode)
            .NotEmpty()
            .Matches(ValidationRegexConstants.PolishPostalCodeRegex)
            .WithMessage(ValidationMessageConstants.PostalCodeValidationMessage);
    }
}
