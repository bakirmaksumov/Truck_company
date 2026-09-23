namespace Truck_company.Services;

public sealed class FileService : IFileService
{
    private static readonly HashSet<string> Extensions = new(StringComparer.OrdinalIgnoreCase)
        { ".jpg", ".jpeg", ".png", ".webp" };
    private static readonly HashSet<string> MimeTypes = new(StringComparer.OrdinalIgnoreCase)
        { "image/jpeg", "image/png", "image/webp" };
    private const long MaxBytes = 5 * 1024 * 1024;
    private readonly IWebHostEnvironment _environment;

    public FileService(IWebHostEnvironment environment) => _environment = environment;

    public async Task<string?> SaveImageAsync(IFormFile? file, CancellationToken cancellationToken = default)
    {
        if (file is null || file.Length == 0) return null;
        var extension = Path.GetExtension(file.FileName);
        if (file.Length > MaxBytes || !Extensions.Contains(extension) || !MimeTypes.Contains(file.ContentType))
            throw new InvalidOperationException("Only JPG, PNG or WEBP images up to 5 MB are allowed.");

        var directory = Path.Combine(_environment.WebRootPath, "uploads");
        Directory.CreateDirectory(directory);
        var name = $"{Guid.NewGuid():N}{extension.ToLowerInvariant()}";
        await using var stream = File.Create(Path.Combine(directory, name));
        await file.CopyToAsync(stream, cancellationToken);
        return $"/uploads/{name}";
    }

    public void DeleteImage(string? relativeUrl)
    {
        if (string.IsNullOrWhiteSpace(relativeUrl) || !relativeUrl.StartsWith("/uploads/", StringComparison.Ordinal)) return;
        var name = Path.GetFileName(relativeUrl);
        var path = Path.Combine(_environment.WebRootPath, "uploads", name);
        if (File.Exists(path)) File.Delete(path);
    }
}
