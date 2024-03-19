namespace API.Features.Directories.CreateDirectory;

public record CreateDirectoryRq
{
    public int? ParentDirId { get; set; } // null = parent directory is in root
    public required string Name { get; set; }
    public required string Path { get; set; }
}

public record CreateDirectoryRs
{
    public required int? Id { get; set; }
}
