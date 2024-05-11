using RecruitmentTask.Ui.ApiConnection;
using RecruitmentTask.Ui.Constants;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Linq;
using System.Threading.Tasks;

namespace RecruitmentTask.Ui.Commands;

internal class DisplayAllPeopleCommand : AsyncCommand
{
    private readonly IApiConnectionService _apiConnectionService;

    public DisplayAllPeopleCommand(IApiConnectionService apiConnectionService)
    {
        _apiConnectionService = apiConnectionService;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        AnsiConsole.WriteLine();

        var peopleResponses = await _apiConnectionService.GetAllPeopleAsync();

        if (peopleResponses.Any())
        {
            var table = new Table()
                .LeftAligned()
                .AddColumn(new TableColumn(new Markup("[yellow]#[/]")).Centered())
                .AddColumn(new TableColumn(new Markup("[yellow]First Name[/]")).Centered())
                .AddColumn(new TableColumn(new Markup("[yellow]Last Name[/]")).Centered())
                .AddColumn(new TableColumn(new Markup("[yellow]Birth Date[/]")).Centered())
                .AddColumn(new TableColumn(new Markup("[yellow]Age[/]")).Centered())
                .AddColumn(new TableColumn(new Markup("[yellow]Phone Number[/]")).Centered())
                .AddColumn(new TableColumn(new Markup("[yellow]Street Name[/]")).Centered())
                .AddColumn(new TableColumn(new Markup("[yellow]House Number[/]")).Centered())
                .AddColumn(new TableColumn(new Markup("[yellow]Apartment Number[/]")).Centered())
                .AddColumn(new TableColumn(new Markup("[yellow]Town[/]")).Centered())
                .AddColumn(new TableColumn(new Markup("[yellow]Postal Code[/]")).Centered());

            int personNumber = 1;

            foreach (var peopleResponse in peopleResponses)
            {
                table.AddRow(
                    personNumber.ToString(),
                    peopleResponse.FirstName,
                    peopleResponse.LastName,
                    peopleResponse.BirthDate.ToString(),
                    peopleResponse.Age.ToString(),
                    peopleResponse.PhoneNumber,
                    peopleResponse.StreetName,
                    peopleResponse.HouseNumber,
                    peopleResponse.ApartmentNumber?.ToString() ?? "-",
                    peopleResponse.Town,
                    peopleResponse.PostalCode);

                personNumber++;
            }

            AnsiConsole.Write(table);
        }
        else
        {
            AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}There are no people to display at the moment.[/]");
        }

        AnsiConsole.WriteLine();

        return 0;
    }
}
