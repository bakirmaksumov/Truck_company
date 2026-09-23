namespace Truck_company.Services;

public sealed class FileService : IFileService
{
    private static readonly HashSet<string> AllowedExtensions = new(StringComparer.OrdinalIgnoreCase)
    {
        ".jpg", ".jpeg", ".png", ".webp"
    };

    private static readonly HashSet<string> AllowedMimeTypes = new(StringComparer.OrdinalIgnoreCase)
    {
        "image/jpeg", "image/png", "image/webp"
    };

    private const long MaxFileSize = 5 * 1024 * 1024;
    private readonly string _uploadRoot;
    private readonly string _requestPath;

    public FileService(IWebHostEnvironment environment, IConfiguration configuration)
    {
        var uploadOptions = configuration.GetSection("Uploads").Get<UploadStorageOptions>() ?? new UploadStorageOptions();
        _uploadRoot = uploadOptions.GetAbsolutePhysicalPath(environment.ContentRootPath);
        _requestPath = string.IsNullOrWhiteSpace(uploadOptions.RequestPath) ? "/uploads" : uploadOptions.RequestPath.Trim();
        if (!_requestPath.StartsWith('/'))
        {
            _requestPath = "/" + _requestPath;
        }

        _requestPath = _requestPath.TrimEnd('/');
    }

    public async Task<string?> SaveImageAsync(IFormFile? file, CancellationToken cancellationToken = default)
    {
        if (file is null || file.Length == 0)
        {
            return null;
        }

        if (file.Length > MaxFileSize)
        {
            throw new InvalidOperationException("File is too large. Maximum allowed size is 5 MB.");
        }

        var safeOriginalName = Path.GetFileName(file.FileName);
        var extension = Path.GetExtension(safeOriginalName).ToLowerInvariant();

        if (!AllowedExtensions.Contains(extension))
        {
            throw new InvalidOperationException("Invalid file extension. Allowed: .jpg, .jpeg, .png, .webp.");
        }

        if (!AllowedMimeTypes.Contains(file.ContentType))
        {
            throw new InvalidOperationException("Invalid file type. Only safe image formats are allowed.");
        }

        EnsureWritableUploadDirectory();

        var uniqueFileName = $"{Guid.NewGuid():N}{extension}";
        var absolutePath = Path.Combine(_uploadRoot, uniqueFileName);

        await using var stream = new FileStream(absolutePath, FileMode.CreateNew, FileAccess.Write, FileShare.None);
        await file.CopyToAsync(stream, cancellationToken);

        return $"{_requestPath}/{uniqueFileName}";
    }

    public void DeleteImage(string? relativeUrl)
    {
        if (string.IsNullOrWhiteSpace(relativeUrl))
        {
            return;
        }

        if (!relativeUrl.StartsWith(_requestPath + "/", StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        var fileName = Path.GetFileName(relativeUrl);
        if (string.IsNullOrWhiteSpace(fileName))
        {
            return;
        }

        var absolutePath = Path.Combine(_uploadRoot, fileName);
        var fullUploadRoot = Path.GetFullPath(_uploadRoot);
        var fullAbsolutePath = Path.GetFullPath(absolutePath);

        if (!fullAbsolutePath.StartsWith(fullUploadRoot, StringComparison.OrdinalIgnoreCase))
        {
            return;
        }

        if (File.Exists(absolutePath))
        {
            File.Delete(absolutePath);
        }
    }

    private void EnsureWritableUploadDirectory()
    {
        try
        {
            Directory.CreateDirectory(_uploadRoot);
            var testFile = Path.Combine(_uploadRoot, $".write-test-{Guid.NewGuid():N}.tmp");
            File.WriteAllText(testFile, "ok");
            File.Delete(testFile);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException("Upload directory is not writable. Configure Uploads:PhysicalPath to a writable folder.", ex);
        }
    }
}
