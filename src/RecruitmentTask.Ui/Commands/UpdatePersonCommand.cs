using Spectre.Console.Cli;
using System;
using System.Threading.Tasks;
using RecruitmentTask.Ui.ApiConnection;
using RecruitmentTask.Api.Controllers.People;
using RecruitmentTask.Ui.Constants;
using Spectre.Console;

namespace RecruitmentTask.Ui.Commands;

internal class UpdatePersonCommand : PersonCommandBase
{
    private readonly IApiConnectionService _apiConnectionService;

    public UpdatePersonCommand(IApiConnectionService apiConnectionService)
    {
        _apiConnectionService = apiConnectionService;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        AnsiConsole.WriteLine();

        var peopleResponses = await _apiConnectionService.GetAllPeopleAsync();

        var personToUpdate = SelectPersonByNumber(peopleResponses);

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]    Current first name value is:[/] [{ColorConstants.HIGHLIGHTED}]{personToUpdate.FirstName}[/]");
        string firstName = ShouldUpdateProperty("First Name") ?
            AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]    Enter first name:[/]")) :
            personToUpdate.FirstName;

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]    Current last name value is:[/] [{ColorConstants.HIGHLIGHTED}]{personToUpdate.LastName}[/]");
        string lastName = ShouldUpdateProperty("Last Name") ? 
            AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]    Enter last name:[/]")) :
            personToUpdate.LastName;

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]    Current birth date value is:[/] [{ColorConstants.HIGHLIGHTED}]{personToUpdate.BirthDate}[/]");
        DateOnly birthDate = ShouldUpdateProperty("Birth Date") ?
            AnsiConsole.Prompt(new TextPrompt<DateOnly>($"[{ColorConstants.REGULAR}]    Enter birth date:[/]").ValidationErrorMessage($"[{ColorConstants.ERROR}]        Enter birth date in date format.[/]")) :
            personToUpdate.BirthDate;

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]    Current phone number value is:[/] [{ColorConstants.HIGHLIGHTED}]{personToUpdate.PhoneNumber}[/]");
        string phoneNumber = ShouldUpdateProperty("Phone Number") ? 
            AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]    Enter phone number:[/]")) :
            personToUpdate.PhoneNumber;

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]    Current street name value is:[/] [{ColorConstants.HIGHLIGHTED}]{personToUpdate.StreetName}[/]");
        string streetName = ShouldUpdateProperty("Street Name") ? 
            AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]    Enter street name:[/]")) :
            personToUpdate.StreetName;

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]    Current house number value is:[/] [{ColorConstants.HIGHLIGHTED}]{personToUpdate.HouseNumber}[/]");
        string houseNumber = ShouldUpdateProperty("House Number") ? 
            AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]    Enter house number:[/]")) :
            personToUpdate.HouseNumber;

        int? apartmentNumber = personToUpdate.ApartmentNumber;

        string currentApartmentNumberValueToDisplay = apartmentNumber?.ToString() ?? "-no value-";
        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]    Current house number value is:[/] [{ColorConstants.HIGHLIGHTED}]{currentApartmentNumberValueToDisplay}[/]");
        if (ShouldUpdateProperty("Apartment Number"))
        {
            apartmentNumber = AnsiConsole.Prompt(new TextPrompt<int?>($"[{ColorConstants.REGULAR}]    Enter apartment number:[/]"));
        }

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]    Current town value is:[/] [{ColorConstants.HIGHLIGHTED}]{personToUpdate.Town}[/]");
        string town = ShouldUpdateProperty("Town") ? 
            AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]    Enter town:[/]")) :
            personToUpdate.Town;

        AnsiConsole.WriteLine();
        AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]    Current postal code is:[/] [{ColorConstants.HIGHLIGHTED}]{personToUpdate.PostalCode}[/]");
        string postalCode = ShouldUpdateProperty("Postal Code") ?
            AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]    Enter postal code:[/]")) :
            personToUpdate.PostalCode;

        var updatePersonRequest = new UpdatePersonRequest(
            personToUpdate.Id,
            firstName,
            lastName,
            birthDate,
            phoneNumber,
            streetName,
            houseNumber,
            apartmentNumber,
            town,
            postalCode);

        var apiResponse = await _apiConnectionService.UpdatePersonAsync(updatePersonRequest);

        AnsiConsole.WriteLine();

        if (apiResponse.IsSuccess)
        {
            AnsiConsole.MarkupLine($"[{ColorConstants.SUCCESS}]    Person updated successfully.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]    Updating person failed. Validation errors:[/]");

            foreach (var validationError in apiResponse.ValidationErrors)
            {
                AnsiConsole.MarkupLineInterpolated($"[{ColorConstants.REGULAR}]    {validationError.PropertyName}: [/][{ColorConstants.ERROR}]{validationError.ErrorMessage}[/]");
            }
        }

        AnsiConsole.WriteLine();

        return 0;
    }

    private bool ShouldUpdateProperty(string propertyName)
    {
        string shouldUpdatePropertyAnswer = AnsiConsole.Prompt(
            new TextPrompt<string>($"[{ColorConstants.REGULAR}]    Do you want to update {propertyName}?[/]")
                .AddChoices(["yes", "no"]));

        bool shouldUpdateProperty = shouldUpdatePropertyAnswer == "yes";

        return shouldUpdateProperty;
    }
}
