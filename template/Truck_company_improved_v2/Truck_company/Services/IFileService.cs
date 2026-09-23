namespace Truck_company.Services;

public interface IFileService
{
    Task<string?> SaveImageAsync(IFormFile? file, CancellationToken cancellationToken = default);
    void DeleteImage(string? relativeUrl);
}
