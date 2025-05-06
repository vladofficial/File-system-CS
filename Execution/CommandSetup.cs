using Itmo.ObjectOrientedProgramming.Lab4.ArgumentValidators;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Parsers;

namespace Itmo.ObjectOrientedProgramming.Lab4.Execution;

public class CommandSetup
{
    public (ICommandParser Parser, CommandRegister Register) GetCommandParser()
    {
        var commandRegister = new CommandRegister();

        CommandParser connect = new CommandParser.CommandHandlerBuilder()
            .SetCommandName("connect")
            .SetArgumentCount(1)
            .AddValueFlag("-m")
            .SetArgumentValidator(new OnePathValidator())
            .AddCommandMode(new LocalConnectCommand())
            .SetDefaultCommand(new LocalConnectCommand())
            .Build();

        commandRegister.AddCommandHandler(connect);

        CommandParser disconnect = new CommandParser.CommandHandlerBuilder()
            .SetCommandName("disconnect")
            .SetArgumentCount(0)
            .SetArgumentValidator(new NoArgumentsValidator())
            .AddCommandMode(new DisconnectCommand())
            .SetDefaultCommand(new DisconnectCommand())
            .Build();

        commandRegister.AddCommandHandler(disconnect);

        CommandParser treeGoto = new CommandParser.CommandHandlerBuilder()
            .SetCommandName("tree goto")
            .SetArgumentCount(1)
            .SetArgumentValidator(new OnePathValidator())
            .AddCommandMode(new TreeGoto())
            .SetDefaultCommand(new TreeGoto())
            .Build();

        commandRegister.AddCommandHandler(treeGoto);

        CommandParser treeList = new CommandParser.CommandHandlerBuilder()
            .SetCommandName("tree list")
            .SetArgumentCount(0)
            .AddValueFlag("-d")
            .SetArgumentValidator(new NoArgumentsOneFlag())
            .AddCommandMode(new TreeList())
            .SetDefaultCommand(new TreeList())
            .Build();

        commandRegister.AddCommandHandler(treeList);

        CommandParser fileShow = new CommandParser.CommandHandlerBuilder()
            .SetCommandName("file show")
            .SetArgumentCount(1)
            .SetArgumentValidator(new OnePathValidator())
            .AddCommandMode(new FIleShowConsole())
            .SetDefaultCommand(new FIleShowConsole())
            .Build();

        commandRegister.AddCommandHandler(fileShow);

        CommandParser fileCopy = new CommandParser.CommandHandlerBuilder()
            .SetCommandName("file copy")
            .SetArgumentCount(2)
            .SetArgumentValidator(new TwoPathsValidator())
            .AddCommandMode(new FileCopy())
            .SetDefaultCommand(new FileCopy())
            .Build();

        commandRegister.AddCommandHandler(fileCopy);

        CommandParser fileDelete = new CommandParser.CommandHandlerBuilder()
            .SetCommandName("file delete")
            .SetArgumentCount(1)
            .SetArgumentValidator(new OnePathValidator())
            .AddCommandMode(new FileDelete())
            .SetDefaultCommand(new FileDelete())
            .Build();

        commandRegister.AddCommandHandler(fileDelete);

        CommandParser fileRename = new CommandParser.CommandHandlerBuilder()
            .SetCommandName("file rename")
            .SetArgumentCount(2)
            .SetArgumentValidator(new TwoPathsValidator())
            .AddCommandMode(new FileRename())
            .SetDefaultCommand(new FileRename())
            .Build();

        commandRegister.AddCommandHandler(fileRename);

        ICommandParser parser = connect.AddNext(disconnect.AddNext(
            treeGoto.AddNext(treeList.AddNext(fileShow.AddNext(fileCopy.AddNext(fileDelete.AddNext(fileRename)))))));

        return (parser, commandRegister);
    }
}