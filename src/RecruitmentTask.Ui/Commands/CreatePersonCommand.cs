using RecruitmentTask.Api.Controllers.People;
using RecruitmentTask.Ui.ApiConnection;
using RecruitmentTask.Ui.Constants;
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
        
        string firstName = AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]    Enter first name:[/]"));
        string lastName = AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]    Enter last name:[/]"));
        DateOnly birthDate = AnsiConsole.Prompt(new TextPrompt<DateOnly>($"[{ColorConstants.REGULAR}]    Enter birth date:[/]").ValidationErrorMessage($"[{ColorConstants.ERROR}]        Enter birth date in date format.[/]"));
        string phoneNumber = AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]    Enter phone number:[/]"));
        string streetName = AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]    Enter street name:[/]"));
        string houseNumber = AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]    Enter house number:[/]"));

        string specifyApartmentNumberAnswer = AnsiConsole.Prompt(
            new TextPrompt<string>($"[{ColorConstants.REGULAR}]    Do you want to specify apartment number?[/]")
                .AddChoices(["yes", "no"]));

        bool specifyApartmentNumber = specifyApartmentNumberAnswer == "yes";

        int? apartmentNumber = null;

        if (specifyApartmentNumber)
        {
            apartmentNumber = AnsiConsole.Prompt(new TextPrompt<int?>($"[{ColorConstants.REGULAR}]    Enter apartment number:[/]")); 
        }

        string town = AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]    Enter town:[/]"));
        string postalCode = AnsiConsole.Prompt(new TextPrompt<string>($"[{ColorConstants.REGULAR}]    Enter postal code:[/]"));

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
            AnsiConsole.MarkupLine($"[{ColorConstants.SUCCESS}]    Person created successfully.[/]");
        }
        else
        {
            AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]    Creating person failed. Validation errors:[/]");

            foreach (var validationError in apiResponse.ValidationErrors)
            {
                AnsiConsole.MarkupLineInterpolated($"[{ColorConstants.REGULAR}]    {validationError.PropertyName}: [/][{ColorConstants.ERROR}]{validationError.ErrorMessage}[/]");
            }
        }

        AnsiConsole.WriteLine();

        return 0;
    }
}
