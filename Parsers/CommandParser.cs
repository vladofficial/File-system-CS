using Itmo.ObjectOrientedProgramming.Lab4.ArgumentValidators;
using Itmo.ObjectOrientedProgramming.Lab4.Commands;
using Itmo.ObjectOrientedProgramming.Lab4.Context;
using Itmo.ObjectOrientedProgramming.Lab4.FileSystems;
using Itmo.ObjectOrientedProgramming.Lab4.Output;

namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers;

public class CommandParser : BaseCommandParser
{
    private CommandParser(
        string name,
        int argumentCount,
        Dictionary<string, ICommandMode> commands,
        IArgumentValidator validator,
        ICommandMode defaultCommandMode,
        List<string> flags)
    {
        ArgumentCount = argumentCount;
        _commands = commands;
        _validator = validator;
        _defaultCommandMode = defaultCommandMode;
        CommandName = name;
        _flags = flags;
    }

    public int ArgumentCount { get; }

    private readonly IArgumentValidator _validator;

    private readonly Dictionary<string, ICommandMode> _commands;

    private readonly ICommandMode _defaultCommandMode;

    private readonly List<string> _flags;

    public override CommandExecutionContext Handle(
        string request,
        CommandExecutionContext context,
        IFileSystem fileSystem,
        CommandRegister commandRegister,
        IOutput output)
    {
        if (commandRegister.TryGetCommand(request) != this)
        {
            if (Next is null)
            {
                return context;
            }

            return Next.Handle(request, context, fileSystem, commandRegister, output);
        }

        var parts = request.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

        var arguments = new List<string>();
        var flags = new Dictionary<string, string>();
        string? modeFlag = null;

        CommandName = CommandName ?? throw new ArgumentException("Name is null");
        string commandName = CommandName;

        int startPosition = commandName.Split(' ').ToList().Count;

        for (int i = startPosition; i < parts.Count; i++)
        {
            string part = parts[i];

            if (part.StartsWith('-'))
            {
                if (part == "-m")
                {
                    if (++i >= parts.Count)
                        throw new ArgumentException("Missing value for mode flag '-m'.");

                    modeFlag = parts[i];
                    if (!_commands.ContainsKey(modeFlag))
                        throw new ArgumentException($"Invalid mode flag value: {modeFlag}");
                }
                else if (_flags.Contains(part))
                {
                    if (++i >= parts.Count)
                        throw new ArgumentException($"Missing value for flag '{part}'.");

                    flags[part] = parts[i];
                }
                else
                {
                    throw new ArgumentException($"Unknown flag: {part}");
                }
            }
            else
            {
                arguments.Add(part);
            }
        }

        if (arguments.Count != ArgumentCount)
        {
            throw new ArgumentException(
                $"Invalid number of arguments for '{commandName}'. Expected {ArgumentCount}, got {arguments.Count}.");
        }

        if (!_validator.Validate(arguments, flags))
        {
            throw new ArgumentException("Invalid arguments or flags");
        }

        string executionMode = modeFlag ?? _defaultCommandMode.Flag;
        if (!_commands.ContainsKey(executionMode))
        {
            throw new ArgumentException($"Invalid or missing mode: {executionMode}");
        }

        ICommandMode commandMode = _commands[executionMode];

        var data = new CommandExecutionData(context, fileSystem, arguments, flags, output);

        return commandMode.Execute(data);
    }

    public class CommandHandlerBuilder
    {
        private readonly Dictionary<string, ICommandMode> _commands;
        private readonly List<string> _flags;
        private ICommandMode? _defaultCommand;
        private IArgumentValidator? _validator;
        private string? _commandName;
        private int? _argumentCount;

        public CommandHandlerBuilder()
        {
            _commands = new Dictionary<string, ICommandMode>();
            _flags = new List<string>();
        }

        public CommandHandlerBuilder SetArgumentCount(int count)
        {
            _argumentCount = count;
            return this;
        }

        public CommandHandlerBuilder SetArgumentValidator(IArgumentValidator validator)
        {
            _validator = validator;
            return this;
        }

        public CommandHandlerBuilder AddCommandMode(ICommandMode commandMode)
        {
            _commands.Add(commandMode.Flag, commandMode);
            return this;
        }

        public CommandHandlerBuilder AddValueFlag(string flag)
        {
            _flags.Add(flag);
            return this;
        }

        public CommandHandlerBuilder SetDefaultCommand(ICommandMode commandMode)
        {
            _commands.TryAdd(commandMode.Flag, commandMode);
            _defaultCommand = commandMode;
            return this;
        }

        public CommandHandlerBuilder SetCommandName(string name)
        {
            _commandName = name;
            return this;
        }

        public CommandParser Build()
        {
            if (_commands.Count == 0)
            {
                throw new ArgumentException("You haven't added any command modes");
            }

            _argumentCount = _argumentCount ??
                             throw new ArgumentException("You haven't specified the amount of arguments");
            _commandName = _commandName ?? throw new ArgumentException("You haven't set the name for you command");
            _defaultCommand =
                _defaultCommand ?? throw new ArgumentException("You haven't the set default command mode");
            _validator = _validator ?? throw new ArgumentException("You haven't set the validator for arguments");
            return new CommandParser(
                _commandName,
                _argumentCount.Value,
                _commands,
                _validator,
                _defaultCommand,
                _flags);
        }
    }
}