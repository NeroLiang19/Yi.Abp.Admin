using System.Reflection;
using Microsoft.Extensions.CommandLineUtils;
using Volo.Abp.DependencyInjection;

namespace Yi.Abp.Tool;

public class CommandInvoker : ISingletonDependency
{
    private readonly IEnumerable<ICommand> _commands;

    public CommandInvoker(IEnumerable<ICommand> commands)
    {
        _commands = commands;
        Application = new CommandLineApplication();
        InitCommand();
    }

    private CommandLineApplication Application { get; }

    private void InitCommand()
    {
        Application.HelpOption("-h|--help");
        Application.VersionOption("-v|--versions", Assembly.GetExecutingAssembly().GetName().Version.ToString());
        foreach (var command in _commands)
        {
            var childrenCommandLineApplication = new CommandLineApplication()
            {
                Name = command.Command,
                Parent = Application,
                Description = command.Description
            };
            Application.Commands.Add(childrenCommandLineApplication);
            command.CommandLineApplication(childrenCommandLineApplication);
        }
    }

    public async Task InvokerAsync(string[] args)
    {
        Application.Execute(args);
    }
}