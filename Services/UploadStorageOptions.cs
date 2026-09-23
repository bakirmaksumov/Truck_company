namespace Truck_company.Services;

public sealed class UploadStorageOptions
{
    public string PhysicalPath { get; set; } = "wwwroot/uploads";
    public string RequestPath { get; set; } = "/uploads";

    public string GetAbsolutePhysicalPath(string contentRootPath)
    {
        if (Path.IsPathRooted(PhysicalPath))
        {
            return Path.GetFullPath(PhysicalPath);
        }

        return Path.GetFullPath(Path.Combine(contentRootPath, PhysicalPath));
    }
}
