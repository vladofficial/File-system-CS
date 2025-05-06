namespace Itmo.ObjectOrientedProgramming.Lab4.Parsers;

public class CommandRegister
{
    public CommandRegister()
    {
        Commands = new Dictionary<string, ICommandParser>();
    }

    public Dictionary<string, ICommandParser> Commands { get; }

    public void AddCommandHandler(ICommandParser parser)
    {
        if (parser.CommandName is null)
        {
            return;
        }

        Commands.TryAdd(parser.CommandName, parser);
    }

    public ICommandParser? TryGetCommand(string request)
    {
        var requestWords = request.Split(' ').ToList();
        foreach (KeyValuePair<string, ICommandParser> pair in Commands)
        {
            var words = pair.Key.Split(' ').ToList();

            if (words.Count > requestWords.Count)
            {
                return null;
            }

            bool isMatch = true;
            for (int i = 0; i < words.Count; i++)
            {
                if (requestWords[i] != words[i])
                {
                    isMatch = false;
                    break;
                }
            }

            if (isMatch)
            {
                return pair.Value;
            }
        }

        return null;
    }
}