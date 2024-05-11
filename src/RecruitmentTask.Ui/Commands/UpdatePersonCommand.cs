using Spectre.Console.Cli;
using System.Threading.Tasks;
using RecruitmentTask.Ui.ApiConnection;
using RecruitmentTask.Api.Controllers.People;
using RecruitmentTask.Ui.Constants;
using Spectre.Console;
using System.Linq;
using RecruitmentTask.Ui.Helpers;

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

        if (!peopleResponses.Any())
        {
            AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}There are no people to remove at the moment.[/]");
            return 0;
        }

        var personToUpdate = SelectPersonByNumber(peopleResponses);

        string firstName = GetUpdatedInformation(personToUpdate.FirstName, "first name");
        string lastName = GetUpdatedInformation(personToUpdate.LastName, "last name");
        var birthDate = GetUpdatedInformation(personToUpdate.BirthDate, "birth date");
        string phoneNumber = GetUpdatedInformation(personToUpdate.PhoneNumber, "phone number");
        string streetName = GetUpdatedInformation(personToUpdate.StreetName, "street name");
        string houseNumber = GetUpdatedInformation(personToUpdate.HouseNumber, "house number");
        int? apartmentNumber = GetUpdatedInformation(personToUpdate.ApartmentNumber, "apartment number");
        string town = GetUpdatedInformation(personToUpdate.Town, "town");
        string postalCode = GetUpdatedInformation(personToUpdate.PostalCode, "postal code");

        AnsiConsole.WriteLine();

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

        if (apiResponse.IsSuccess)
        {
            AnsiConsole.MarkupLine($"[{ColorConstants.SUCCESS}]{StylisticConstants.TAB}Person updated successfully.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}Updating person failed. Validation errors:[/]");

            foreach (var validationError in apiResponse.ValidationErrors)
            {
                AnsiConsole.MarkupLineInterpolated($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}{validationError.PropertyName}: [/][{ColorConstants.ERROR}]{validationError.ErrorMessage}[/]");
            }
        }

        AnsiConsole.WriteLine();

        return 0;
    }

    private TInformation GetUpdatedInformation<TInformation>(TInformation currentInformation, string informationName)
    {
        AnsiConsole.WriteLine();

        string currentInformationDisplay = currentInformation?.ToString() ?? "-no value-";

        AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}Current {informationName} is:[/] [{ColorConstants.HIGHLIGHTED}]{currentInformationDisplay}[/]");

        bool shouldUpdateInformation = ShouldUpdateInformation(informationName);

        var information = shouldUpdateInformation ? 
            AnsiConsole.Prompt(new TextPrompt<TInformation>($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}Enter {informationName}:[/]")) :
            currentInformation;

        return information;
    }

    private bool ShouldUpdateInformation(string informationName)
    {
        bool shouldUpdateInformation = ConsoleHelper.AskYesNoQuestion($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}Do you want to update {informationName}?[/]");

        return shouldUpdateInformation;
    }
}
