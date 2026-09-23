using Microsoft.AspNetCore.Identity;

namespace Truck_company.Models;

public class ApplicationUser : IdentityUser
{
    public string? FullName { get; set; }
}
