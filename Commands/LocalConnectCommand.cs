using Itmo.ObjectOrientedProgramming.Lab4.Context;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class LocalConnectCommand : ICommandMode
{
    public string Flag { get; } = "local";

    public CommandExecutionContext Execute(CommandExecutionData data)
    {
        string path = data.Arguments.First();
        data.Context.UpdateCurrentPath(path);
        data.Context.UpdateTreePath(path);
        return data.Context;
    }
}