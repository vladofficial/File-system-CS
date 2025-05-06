namespace Itmo.ObjectOrientedProgramming.Lab4.ArgumentValidators;

public class PathAndIntegerFlagValidator : IArgumentValidator
{
    public bool Validate(IList<string> arguments, IDictionary<string, string> flags)
    {
        if (arguments.Count != 1 || !flags.ContainsKey("-d"))
        {
            return false;
        }

        if (arguments[0].IndexOfAny(Path.GetInvalidPathChars()) != -1)
        {
            return false;
        }

        if (!int.TryParse(flags["-d"], out _))
        {
            return false;
        }

        return true;
    }
}