using Itmo.ObjectOrientedProgramming.Lab4.Context;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileCopy : ICommandMode
{
    public string Flag { get; } = "default";

    public CommandExecutionContext Execute(CommandExecutionData data)
    {
        string source = data.Arguments[0];
        string destination = data.Arguments[1];
        data.FileSystem.CopyFile(source, destination);
        return data.Context;
    }
}