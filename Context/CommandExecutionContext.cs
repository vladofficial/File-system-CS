namespace Itmo.ObjectOrientedProgramming.Lab4.Context;

public class CommandExecutionContext
{
    public CommandExecutionContext()
    {
        CurrentPath = string.Empty;
        CurrenctTreePath = string.Empty;
    }

    public void UpdateCurrentPath(string newPath)
    {
        newPath = NormalizePath(newPath);
        if (Path.IsPathRooted(newPath) || IsWindowsStyleRooted(newPath))
        {
            CurrentPath = newPath;
        }
        else
        {
            CurrentPath = Path.Combine(CurrentPath, newPath);
        }
    }

    public void UpdateTreePath(string newPath)
    {
        newPath = NormalizePath(newPath);
        if (Path.IsPathRooted(newPath) || IsWindowsStyleRooted(newPath))
        {
            CurrenctTreePath = newPath;
        }
        else
        {
            CurrenctTreePath = Path.Combine(CurrenctTreePath, newPath);
        }
    }

    public void ResetCurrentPath()
    {
        CurrentPath = string.Empty;
    }

    public void ResetTreePath()
    {
        CurrenctTreePath = string.Empty;
    }

    private bool IsWindowsStyleRooted(string path)
    {
        return path.Length > 1 && char.IsLetter(path[0]) && path[1] == ':';
    }

    private string NormalizePath(string path)
    {
        return path.Replace('\\', '/');
    }

    public string CurrentPath { get; private set; }

    public string CurrenctTreePath { get; private set; }
}