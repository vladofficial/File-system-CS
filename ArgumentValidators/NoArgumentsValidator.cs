namespace Itmo.ObjectOrientedProgramming.Lab4.ArgumentValidators;

public class NoArgumentsValidator : IArgumentValidator
{
    public bool Validate(IList<string> arguments, IDictionary<string, string> flags)
    {
        if (arguments.Count != 0)
        {
            return false;
        }

        return true;
    }
}