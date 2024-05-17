using RecruitmentTask.Ui.ApiConnection;
using RecruitmentTask.Ui.Constants;
using Spectre.Console;
using Spectre.Console.Cli;
using System.Threading.Tasks;

namespace RecruitmentTask.Ui.Commands.Base;

internal abstract class ApiCommandBase : AsyncCommand
{
    protected readonly IApiConnectionService _apiConnectionService;

    public ApiCommandBase(IApiConnectionService apiConnectionService)
    {
        _apiConnectionService = apiConnectionService;
    }

    public async Task<bool> IsApiAvailableAsync()
    {
        bool isApiAvailableAsync = await _apiConnectionService.IsApiAvailableAsync();

        return isApiAvailableAsync;
    }

    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        bool isApiAvailable = await IsApiAvailableAsync();

        if (!isApiAvailable)
        {
            AnsiConsole.MarkupLine($"[{ColorConstants.ERROR}]{StylisticConstants.TAB}It seems like the server of the application is unavailable at the moment, please try again later.[/]");
            AnsiConsole.WriteLine();

            return 1;
        }

        return 0;
    }
}
