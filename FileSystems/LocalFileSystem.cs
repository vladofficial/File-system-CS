namespace Itmo.ObjectOrientedProgramming.Lab4.FileSystems;

public class LocalFileSystem : IFileSystem
{
    public string DirectorySymbol { get; set; } = "[DIR]";

    public string FileSymbol { get; set; } = "[FILE]";

    public IEnumerable<string> GetDirectoryTree(
        string path,
        int depth = 1)
    {
        if (!Directory.Exists(path))
            throw new DirectoryNotFoundException($"Directory '{path}' does not exist.");

        var root = new FileSystemNode(Path.GetFileName(path), isDirectory: true);

        PopulateDirectoryTree(root, path, depth);

        IEnumerable<string> result = GetTree(root, DirectorySymbol, FileSymbol);
        return result;
    }

    public bool DirectoryExists(string path)
    {
        return Directory.Exists(path);
    }

    public string ReadFile(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"File '{path}' does not exist.");

        return File.ReadAllText(path);
    }

    public void CopyFile(string sourcePath, string destinationPath)
    {
        if (!File.Exists(sourcePath))
            throw new FileNotFoundException($"File '{sourcePath}' does not exist.");

        File.Copy(sourcePath, destinationPath, overwrite: true);
    }

    public void MoveFile(string sourcePath, string destinationPath)
    {
        if (!File.Exists(sourcePath))
            throw new FileNotFoundException($"File '{sourcePath}' does not exist.");

        File.Move(sourcePath, destinationPath);
    }

    public void DeleteFile(string path)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"File '{path}' does not exist.");

        File.Delete(path);
    }

    public void RenameFile(string path, string newName)
    {
        if (!File.Exists(path))
            throw new FileNotFoundException($"File '{path}' does not exist.");

        string directory = Path.GetDirectoryName(path) ?? throw new InvalidOperationException("Invalid file path.");
        string newPath = Path.Combine(directory, newName);

        File.Move(path, newPath);
    }

    public bool FileExists(string path)
    {
        return File.Exists(path);
    }

    public class FileSystemNode
    {
        public string Name { get; }

        public bool IsDirectory { get; }

        public IList<FileSystemNode> Children { get; }

        public FileSystemNode(string name, bool isDirectory)
        {
            Name = name;
            IsDirectory = isDirectory;
            Children = new List<FileSystemNode>();
        }
    }

    private void PopulateDirectoryTree(FileSystemNode node, string path, int depth)
    {
        if (depth < 0) return;

        foreach (string directory in Directory.GetDirectories(path))
        {
            var dirNode = new FileSystemNode(Path.GetFileName(directory), isDirectory: true);
            node.Children.Add(dirNode);

            PopulateDirectoryTree(dirNode, directory, depth - 1);
        }

        foreach (string file in Directory.GetFiles(path))
        {
            var fileNode = new FileSystemNode(Path.GetFileName(file), isDirectory: false);
            node.Children.Add(fileNode);
        }
    }

    private List<string> GetTree(
        FileSystemNode node,
        string directorySymbol = "[DIR]",
        string fileSymbol = "[FILE]",
        int indent = 0)
    {
        var lines = new List<string>();
        string symbol = node.IsDirectory ? directorySymbol : fileSymbol;

        lines.Add(new string(' ', indent * 2) + $"{symbol} {node.Name}");

        foreach (FileSystemNode child in node.Children)
        {
            lines.AddRange(GetTree(child, directorySymbol, fileSymbol, indent + 1));
        }

        return lines;
    }
}