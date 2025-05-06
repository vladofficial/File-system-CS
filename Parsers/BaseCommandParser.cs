using Itmo.ObjectOrientedProgramming.Lab4.Context;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Output;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers;

public abstract class BaseCommandParser : ICommandParser
{
    public string? CommandName { get; protected set; }

    public ICommandParser AddNext(ICommandParser parser)
    {
        if (Next is null)
        {
            Next = parser;
        }
        else
        {
            Next.AddNext(parser);
        }

        return this;
    }

    public abstract CommandExecutionContext Handle(
        string request,
        CommandExecutionContext context,
        IFileSystem fileSystem,
        CommandRegister commandRegister,
        IOutput output);

    protected ICommandParser? Next { get; private set; }
}