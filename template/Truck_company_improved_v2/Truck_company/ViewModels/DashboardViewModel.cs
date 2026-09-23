using Truck_company.Models;

namespace Truck_company.ViewModels;

public class DashboardViewModel
{
    public int TotalServices { get; set; }
    public int NewQuoteRequests { get; set; }
    public int TotalMessages { get; set; }
    public int ActiveFeatures { get; set; }
    public List<QuoteRequest> RecentQuoteRequests { get; set; } = new();
}

public class AdminSettingsViewModel
{
    public SiteSettings SiteSettings { get; set; } = new();
    public HeroSection HeroSection { get; set; } = new();
    public AboutSection AboutSection { get; set; } = new();
    public ContactSettings ContactSettings { get; set; } = new();
}
