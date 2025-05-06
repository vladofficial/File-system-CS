using Itmo.ObjectOrientedProgramming.Lab4.Context;

namespace Itmo.ObjectOrientedProgramming.Lab4.Commands;

public class FIleShowConsole : ICommandMode
{
    public string Flag { get; } = "console";

    public CommandExecutionContext Execute(CommandExecutionData data)
    {
        string path = data.Arguments.First();
        IEnumerable<string> fileContents = new List<string> { data.FileSystem.ReadFile(path) };
        data.Output.Output(fileContents);
        return data.Context;
    }
}