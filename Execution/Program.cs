using Itmo.ObjectOrientedProgramming.Lab4.Context;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Output;
using Itmo.ObjectOrientedProgramming.Lab4.Parsers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Execution;

public class Program
{
    public void Run()
    {
        ICommandParser parser;
        CommandRegister commandRegister;
        (parser, commandRegister) = new CommandSetup().GetCommandParser();

        while (true)
        {
            string? request = Console.ReadLine();
            if (request is null)
            {
                return;
            }

            CommandExecutionContext context = new();

            IFileSystem fileSystem = new LocalFileSystem();
            IOutput output = new ConsoleOutput();

            context = parser.Handle(request, context, fileSystem, commandRegister, output);
        }
    }
}