namespace Itmo.ObjectOrientedProgramming.Lab4.ArgumentValidators;

public interface IArgumentValidator
{
    public bool Validate(IList<string> arguments, IDictionary<string, string> flags);
}