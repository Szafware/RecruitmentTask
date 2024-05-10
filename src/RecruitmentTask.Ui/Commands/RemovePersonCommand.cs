using Spectre.Console.Cli;
using System.Threading.Tasks;
using Spectre.Console;
using RecruitmentTask.Ui.ApiConnection;
using System.Linq;
using RecruitmentTask.Ui.Constants;

namespace RecruitmentTask.Ui.Commands;

internal class RemovePersonCommand : PersonCommandBase
{
    private readonly IApiConnectionService _apiConnectionService;

    public RemovePersonCommand(IApiConnectionService apiConnectionService)
    {
        _apiConnectionService = apiConnectionService;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        AnsiConsole.WriteLine();

        var peopleResponses = await _apiConnectionService.GetAllPeopleAsync();

        if (peopleResponses.Any())
        {
            var personToRemove = SelectPersonByNumber(peopleResponses);

            var apiResponse = await _apiConnectionService.RemovePerson(personToRemove.Id);

            AnsiConsole.WriteLine();

            string message = apiResponse.IsSuccess ? $"[{ColorConstants.SUCCESS}]    Person removed successfully.[/]" : $"[{ColorConstants.ERROR}]    Removing person failed[/]";

            AnsiConsole.MarkupLine(message);
        }
        else
        {
            AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]    There are no people to remove at the moment.[/]");
        }

        AnsiConsole.WriteLine();

        return 0;
    }
}
