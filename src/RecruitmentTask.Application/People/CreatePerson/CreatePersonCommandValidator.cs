using FluentValidation;

namespace RecruitmentTask.Application.People.CreatePerson;

public class CreatePersonCommandValidator : AbstractValidator<CreatePersonCommand>
{
    public CreatePersonCommandValidator()
    {
        RuleFor(person => person.FirstName).NotEmpty().MinimumLength(3);
        RuleFor(person => person.LastName).NotEmpty().MinimumLength(3);

        // TODO: Fill the rest of properties
    }
}
