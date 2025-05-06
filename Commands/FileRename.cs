using Itmo.ObjectOrientedProgramming.Lab4.Context;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FileRename : ICommandMode
{
    public string Flag { get; } = "default";

    public CommandExecutionContext Execute(CommandExecutionData data)
    {
        string path = data.Arguments[0];
        string newName = data.Arguments[1];
        data.FileSystem.RenameFile(path, newName);
        return data.Context;
    }
}