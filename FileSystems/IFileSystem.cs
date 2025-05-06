namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

public interface IFileSystem
{
    public string DirectorySymbol { get; set; }

    public string FileSymbol { get; set; }

    public IEnumerable<string> GetDirectoryTree(string path, int depth = 1);

    public bool DirectoryExists(string path);

    public bool FileExists(string path);

    public string ReadFile(string path);

    public void CopyFile(string sourcePath, string destinationPath);

    public void MoveFile(string sourcePath, string destinationPath);

    public void DeleteFile(string path);

    public void RenameFile(string path, string newName);
}