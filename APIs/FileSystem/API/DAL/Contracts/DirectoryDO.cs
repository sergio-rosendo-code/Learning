namespace API.DAL.Contracts;

public record DirectoryDo
{
    public required int? Id { get; set; }
    public required string Name { get; set; }
    public required string Path { get; set; }
    public required int Size { get; set; } // Size in bytes
    public required DateTime Create { get; set; }
    public required DateTime Updated { get; set; }
}