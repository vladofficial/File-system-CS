using Itmo.ObjectOrientedProgramming.Lab4.Context;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileDelete : ICommandMode
{
    public string Flag { get; } = "default";

    public CommandExecutionContext Execute(CommandExecutionData data)
    {
        string path = data.Arguments[0];
        data.FileSystem.DeleteFile(path);
        return data.Context;
    }
}