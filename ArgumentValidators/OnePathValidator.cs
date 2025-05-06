namespace Itmo.ObjectOrientedProgramming.Lab4.ArgumentValidators;

public class OnePathValidator : IArgumentValidator
{
    public bool Validate(IList<string> arguments, IDictionary<string, string> flags)
    {
        if (arguments.Count != 1 || arguments[0].IndexOfAny(Path.GetInvalidPathChars()) != -1)
        {
            return false;
        }

        return true;
    }
}