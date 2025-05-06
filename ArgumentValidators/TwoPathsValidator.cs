namespace Itmo.ObjectOrientedProgramming.Lab4.ArgumentValidators;

public class TwoPathsValidator : IArgumentValidator
{
    public bool Validate(IList<string> arguments, IDictionary<string, string> flags)
    {
        if (arguments.Count != 2)
        {
            return false;
        }

        if (arguments[0].IndexOfAny(Path.GetInvalidPathChars()) != -1 ||
            arguments[1].IndexOfAny(Path.GetInvalidPathChars()) != -1)
        {
            return false;
        }

        return true;
    }
}