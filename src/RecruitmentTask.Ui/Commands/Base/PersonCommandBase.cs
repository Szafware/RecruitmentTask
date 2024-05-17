using RecruitmentTask.Application.People.GetAllPeople;
using RecruitmentTask.Ui.ApiConnection;
using RecruitmentTask.Ui.Constants;
using Spectre.Console;
using System.Collections.Generic;
using System.Linq;

namespace RecruitmentTask.Ui.Commands.Base;

internal abstract class PersonCommandBase : ApiCommandBase
{
    protected PersonCommandBase(IApiConnectionService apiConnectionService) : base(apiConnectionService)
    {
    }

    protected PersonResponse SelectPersonByNumber(IEnumerable<PersonResponse> peopleResponses)
    {
        var table = new Table()
            .LeftAligned()
            .AddColumn(new TableColumn(new Markup($"[{ColorConstants.REGULAR}]Number[/]")).Centered())
            .AddColumn(new TableColumn(new Markup($"[{ColorConstants.REGULAR}]First Name[/]")).Centered())
            .AddColumn(new TableColumn(new Markup($"[{ColorConstants.REGULAR}]Last Name[/]")).Centered());

        int personNumber = 1;

        foreach (var peopleResponse in peopleResponses)
        {
            table.AddRow(
                personNumber.ToString(),
                peopleResponse.FirstName,
                peopleResponse.LastName);

            personNumber++;
        }

        AnsiConsole.Write(table);

        AnsiConsole.WriteLine();

        var peopleNumbers = Enumerable.Range(1, peopleResponses.Count());

        int selectedPersonNumber = AnsiConsole.Prompt(
            new TextPrompt<int>($"[{ColorConstants.REGULAR}]{StylisticConstants.TAB}Enter number of person to be removed:[/]")
                .AddChoices(peopleNumbers)
                .ValidationErrorMessage($"[{ColorConstants.ERROR}]{StylisticConstants.TAB}Enter id in guid format.[/]"));

        int selectedPersonIndex = selectedPersonNumber - 1;

        var selectedPerson = peopleResponses.ToList()[selectedPersonIndex];

        return selectedPerson;
    }
}
