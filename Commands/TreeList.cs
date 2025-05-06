using Itmo.ObjectOrientedProgramming.Lab4.Context;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class TreeList : ICommandMode
{
    public string Flag { get; } = "default";

    public CommandExecutionContext Execute(CommandExecutionData data)
    {
        int depth = 1;
        if (data.Flags.ContainsKey("-d"))
        {
            depth = int.Parse(data.Flags["-d"]);
        }

        IEnumerable<string> tree = data.FileSystem.GetDirectoryTree(data.Context.CurrenctTreePath, depth);

        data.Output.Output(tree);

        return data.Context;
    }
}