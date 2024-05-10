using Spectre.Console;
using Spectre.Console.Cli;

namespace RecruitmentTask.Ui.Commands;

internal class ClearCommand : Command
{
    public override int Execute(CommandContext context)
    {
        AnsiConsole.Clear();

        Program.DisplayStartInfo();

        return 0;
    }
}
