using Microsoft.Extensions.DependencyInjection;
using RecruitmentTask.Ui.ApiConnection;
using RecruitmentTask.Ui.Commands;
using RecruitmentTask.Ui.Configuration;
using RecruitmentTask.Ui.Constants;
using Spectre.Console;
using Spectre.Console.Cli;
using System;
using System.Threading.Tasks;

internal class Program
{
    private static CommandApp _application;

    private static async Task Main(string[] args)
    {
        _application = new CommandApp(RegisterServices());

        _application.Configure(configuration =>
        {
            configuration.AddCommand<DisplayAllPeopleCommand>("display");
            configuration.AddCommand<CreatePersonCommand>("create");
            configuration.AddCommand<UpdatePersonCommand>("update");
            configuration.AddCommand<RemovePersonCommand>("remove");
            configuration.AddCommand<ClearCommand>("clear");
            configuration.AddCommand<ExitCommand>("exit");
        });

        DisplayStartInfo();

        while (true)
        {
            AnsiConsole.Markup($"[{ColorConstants.PROMPT}]executing command $[/]: ");

            string input = Console.ReadLine();

            await _application.RunAsync(input.Split(' '));
        }
    }

    private static ITypeRegistrar RegisterServices()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddScoped<IApiConnectionService, ApiConnectionService>();

        var typeRegistrar = new TypeRegistrar(serviceCollection);

        return typeRegistrar;
    }

    internal static void DisplayStartInfo()
    {
        AnsiConsole.Write(new FigletText("Recruitment Task").LeftJustified().Color(Color.Teal));

        _application.Run(["-h"]);
        AnsiConsole.WriteLine();
    }
}
