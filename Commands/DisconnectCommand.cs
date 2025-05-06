using Itmo.ObjectOrientedProgramming.Lab4.Context;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class DisconnectCommand : ICommandMode
{
    public string Flag { get; } = "default";

    public CommandExecutionContext Execute(CommandExecutionData data)
    {
        data.Context.ResetCurrentPath();
        data.Context.ResetTreePath();
        return data.Context;
    }
}