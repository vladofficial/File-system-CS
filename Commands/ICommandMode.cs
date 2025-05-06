using Itmo.ObjectOrientedProgramming.Lab4.Context;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public interface ICommandMode
{
    public string Flag { get; }

    public CommandExecutionContext Execute(CommandExecutionData data);
}