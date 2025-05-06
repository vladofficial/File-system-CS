using Itmo.ObjectOrientedProgramming.Lab4.Context;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Output;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers;

public interface ICommandParser
{
    public string? CommandName { get; }

    public CommandExecutionContext Handle(
        string request,
        CommandExecutionContext context,
        IFileSystem fileSystem,
        CommandRegister commandRegister,
        IOutput output);

    ICommandParser AddNext(ICommandParser parser);
}