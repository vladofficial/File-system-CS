namespace Itmo.ObjectOrientedProgramming.Lab4.Output;

public class ConsoleOutput : IOutput
{
    public void Output(IEnumerable<string> data)
    {
        foreach (string line in data)
        {
            Console.WriteLine(line);
        }
    }
}