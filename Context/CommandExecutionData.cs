using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Output;

namespace Itmo.ObjectOrientedProgramming.Lab4.Context;

public class CommandExecutionData
{
    public CommandExecutionData(
        CommandExecutionContext context,
        IFileSystem fileSystem,
        IList<string> arguments,
        IDictionary<string, string> flags,
        IOutput output)
    {
        Context = context;
        FileSystem = fileSystem;
        Arguments = arguments;
        Flags = flags;
        Output = output;
    }

    public CommandExecutionContext Context { get; }

    public IFileSystem FileSystem { get; }

    public IList<string> Arguments { get; }

    public IDictionary<string, string> Flags { get; }

    public IOutput Output { get; }
}