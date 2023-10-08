// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.Configuration;
using SavaDev.Infrastructure;
using SavaDev.MigrationsMaker;

Console.WriteLine("Hello, World!");

IConfigurationRoot config = ConfigFile.GetConfiguration(ConfigFile.GetEnvironment());

var solution = new Solution(config["SolutionName"], config["SolutionPath"]);
var startupProject = new StartupProject(config["StartupProject"], solution.SolutionPath);
var migrationProjects = solution.GetMigrationProjects();
var services = solution.GetSolutionServices();

var cliArgs = Environment.GetCommandLineArgs().Skip(1);

if (cliArgs.Count() == 0)
{
    Console.Write("Command: ");
    var input = Console.ReadLine();
    if (string.IsNullOrEmpty(input)) return;

    cliArgs = input.Trim().Split(" ");   
}

var profile = new MigrationsProfile(cliArgs, services, config["ModelHasChangesOnly"] == "true");
var executor = new MigrationsExecutor(startupProject, migrationProjects, profile);
executor.Run();

Console.WriteLine("Finish");
Console.ReadKey();



