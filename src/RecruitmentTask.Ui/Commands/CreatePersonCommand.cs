using RecruitmentTask.Api.Controllers.People;
using RecruitmentTask.Ui.ApiConnection;
using RecruitmentTask.Ui.Constants;
using RecruitmentTask.Ui.Helpers;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Threading.Tasks;

namespace RecruitmentTask.Ui.Commands;

internal class CreatePersonCommand : AsyncCommand
{
    private readonly IApiConnectionService _apiConnectionService;

    public CreatePersonCommand(IApiConnectionService apiConnectionService)
    {
        _apiConnectionService = apiConnectionService;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        AnsiConsole.WriteLine();
        
        string firstName = AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}Enter first name:[/]"));
        string lastName = AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}Enter last name:[/]"));
        DateOnly birthDate = AnsiConsole.Prompt(new TextPrompt<DateOnly>($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}Enter birth date:[/]").ValidationErrorMessage($"[{ColorConstants.ERROR}]{StylisticConstants.TAB}Enter birth date in date format.[/]"));
        string phoneNumber = AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}Enter phone number:[/]"));
        string streetName = AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}Enter street name:[/]"));
        string houseNumber = AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}Enter house number:[/]"));
        
        bool specifyApartmentNumber = ConsoleHelper.AskYesNoQuestion($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}Do you want to specify apartment number?[/]");

        int? apartmentNumber = null;

        if (specifyApartmentNumber)
        {
            apartmentNumber = AnsiConsole.Prompt(new TextPrompt<int?>($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}Enter apartment number:[/]")); 
        }

        string town = AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}Enter town:[/]"));
        string postalCode = AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}Enter postal code:[/]"));

        var createPersonRequest = new CreatePersonRequest(
            firstName,
            lastName,
            birthDate,
            phoneNumber,
            streetName,
            houseNumber,
            apartmentNumber,
            town,
            postalCode);

        var apiResponse = await _apiConnectionService.CreatePersonAsync(createPersonRequest);

        AnsiConsole.WriteLine();

        if (apiResponse.IsSuccess)
        {
            AnsiConsole.MarkupLine($"[{ColorConstants.SUCCESS}]{StylisticConstants.TAB}Person created successfully.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}Creating person failed. Validation errors:[/]");

            foreach (var validationError in apiResponse.ValidationErrors)
            {
                AnsiConsole.MarkupLineInterpolated($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}{validationError.PropertyName}: [/][{ColorConstants.ERROR}]{validationError.ErrorMessage}[/]");
            }
        }

        AnsiConsole.WriteLine();

        return 0;
    }
}
