using Spectre.Console.Cli;
using System;

namespace RecruitmentTask.Ui.Commands;

internal class ExitCommand : Command
{
    public override int Execute(CommandContext context)
    {
        Environment.Exit(0);
        return 0;
    }
}
