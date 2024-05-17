using Spectre.Console.Cli;
using System.Threading.Tasks;
using Spectre.Console;
using RecruitmentTask.Ui.ApiConnection;
using System.Linq;
using RecruitmentTask.Ui.Constants;
using RecruitmentTask.Ui.Commands.Base;

namespace RecruitmentTask.Ui.Commands;

internal class RemovePersonCommand : PersonCommandBase
{
    public RemovePersonCommand(IApiConnectionService apiConnectionService) : base(apiConnectionService)
    {
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        AnsiConsole.WriteLine();

        if (await base.ExecuteAsync(context) == 1)
        {
            return 0;
        }

        var peopleResponses = await _apiConnectionService.GetAllPeopleAsync();

        if (peopleResponses.Any())
        {
            var personToRemove = SelectPersonByNumber(peopleResponses);

            var apiResponse = await _apiConnectionService.RemovePersonAsync(personToRemove.Id);

            AnsiConsole.WriteLine();

            string message = apiResponse.IsSuccess ? $"[{ColorConstants.SUCCESS}]{StylisticConstants.TAB}Person removed successfully.[/]" : $"[{ColorConstants.ERROR}]{StylisticConstants.TAB}Removing person failed[/]";

            AnsiConsole.MarkupLine(message);
        }
        else
        {
            AnsiConsole.MarkupLine($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}There are no people to remove at the moment.[/]");
        }

        AnsiConsole.WriteLine();

        return 0;
    }
}
