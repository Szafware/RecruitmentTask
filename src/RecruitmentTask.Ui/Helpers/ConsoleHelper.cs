using Spectre.Console;

namespace RecruitmentTask.Ui.Helpers;

internal static class ConsoleHelper
{
    public static bool AskYesNoQuestion(string question)
    {
        const string confirmationAnswer = "yes";
        const string refusalAnswer = "no";

        string questionAnswer = AnsiConsole.Prompt(new TextPrompt<string>(question).AddChoices([confirmationAnswer, refusalAnswer]));

        bool isConfirmed = questionAnswer == confirmationAnswer;

        return isConfirmed;
    }
}
