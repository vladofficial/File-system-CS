namespace Itmo.ObjectOrientedProgramming.Lab4.ArgumentValidators;

public class NoArgumentsOneFlag : IArgumentValidator
{
    public bool Validate(IList<string> arguments, IDictionary<string, string> flags)
    {
        if (arguments.Count != 0)
        {
            return false;
        }

        if (flags.Count != 1)
        {
            return false;
        }

        return true;
    }
}