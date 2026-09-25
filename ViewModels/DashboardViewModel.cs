using Truck_company.Models;
using System.ComponentModel.DataAnnotations;

namespace Truck_company.ViewModels;

public class DashboardViewModel
{
    public int TotalServices { get; set; }
    public int ActiveServices { get; set; }
    public int NewQuoteRequests { get; set; }
    public int TotalQuoteRequests { get; set; }
    public int TotalMessages { get; set; }
    public int ActiveFeatures { get; set; }
    public List<QuoteRequest> RecentQuoteRequests { get; set; } = new();
    public List<ContactMessage> RecentContactMessages { get; set; } = new();
}

public class AdminSettingsInputViewModel
{
    public SiteSettingsInputViewModel SiteSettings { get; set; } = new();
    public HeroSectionInputViewModel HeroSection { get; set; } = new();
    public AboutSectionInputViewModel AboutSection { get; set; } = new();
    public ContactSettingsInputViewModel ContactSettings { get; set; } = new();

    public string? CurrentLogoUrl { get; set; }
    public string? CurrentHeroImageUrl { get; set; }
    public string? CurrentAboutImageUrl { get; set; }
    public string? CurrentOpenGraphImageUrl { get; set; }
}

public class SiteSettingsInputViewModel
{
    [Required, StringLength(160)]
    public string CompanyName { get; set; } = string.Empty;
    [Required, StringLength(60)]
    public string LogoText { get; set; } = string.Empty;
    [Required, StringLength(60)]
    public string LogoSubText { get; set; } = string.Empty;
    [Required, StringLength(180)]
    public string Tagline { get; set; } = string.Empty;
    [Required, Phone, StringLength(40)]
    public string PrimaryPhone { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(160)]
    public string Email { get; set; } = string.Empty;
    [Required, StringLength(220)]
    public string Address { get; set; } = string.Empty;
    [Required, StringLength(220)]
    public string FooterText { get; set; } = string.Empty;
    [Required, StringLength(220)]
    public string CopyrightText { get; set; } = string.Empty;
    [Required, StringLength(160)]
    public string MetaTitle { get; set; } = string.Empty;
    [Required, StringLength(320)]
    public string MetaDescription { get; set; } = string.Empty;
    [Required, StringLength(320)]
    public string MetaKeywords { get; set; } = string.Empty;
    [Required, StringLength(160)]
    public string OpenGraphTitle { get; set; } = string.Empty;
    [Required, StringLength(320)]
    public string OpenGraphDescription { get; set; } = string.Empty;
}

public class HeroSectionInputViewModel
{
    [Required, StringLength(120)]
    public string Title { get; set; } = string.Empty;
    [Required, StringLength(120)]
    public string HighlightTitle { get; set; } = string.Empty;
    [Required, StringLength(1200)]
    public string Description { get; set; } = string.Empty;
    [StringLength(600)]
    public string BackgroundImageUrl { get; set; } = string.Empty;
    [Required, StringLength(60)]
    public string PrimaryButtonText { get; set; } = string.Empty;
    [Required, StringLength(250)]
    public string PrimaryButtonUrl { get; set; } = string.Empty;
    [Required, StringLength(60)]
    public string SecondaryButtonText { get; set; } = string.Empty;
    [Required, StringLength(250)]
    public string SecondaryButtonUrl { get; set; } = string.Empty;
}

public class AboutSectionInputViewModel
{
    [Required, StringLength(160)]
    public string Title { get; set; } = string.Empty;
    [Required, StringLength(220)]
    public string Subtitle { get; set; } = string.Empty;
    [Required, StringLength(2000)]
    public string Description { get; set; } = string.Empty;
    [StringLength(600)]
    public string ImageUrl { get; set; } = string.Empty;
    [Required, StringLength(80)]
    public string MissionTitle { get; set; } = string.Empty;
    [Required, StringLength(400)]
    public string MissionDescription { get; set; } = string.Empty;
    [Required, StringLength(80)]
    public string VisionTitle { get; set; } = string.Empty;
    [Required, StringLength(400)]
    public string VisionDescription { get; set; } = string.Empty;
    [Required, StringLength(80)]
    public string ValuesTitle { get; set; } = string.Empty;
    [Required, StringLength(400)]
    public string ValuesDescription { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;
}

public class ContactSettingsInputViewModel
{
    [Required, StringLength(160)]
    public string CompanyName { get; set; } = string.Empty;
    [Required, StringLength(220)]
    public string Address { get; set; } = string.Empty;
    [Required, Phone, StringLength(40)]
    public string Phone { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(160)]
    public string Email { get; set; } = string.Empty;
    [Required, StringLength(120)]
    public string BusinessHours { get; set; } = string.Empty;
    [Required, StringLength(700)]
    public string MapUrl { get; set; } = string.Empty;
    [StringLength(250)]
    public string FacebookUrl { get; set; } = string.Empty;
    [StringLength(250)]
    public string InstagramUrl { get; set; } = string.Empty;
    [StringLength(250)]
    public string LinkedInUrl { get; set; } = string.Empty;
}

public class ServiceInputViewModel
{
    public int Id { get; set; }
    [Required, StringLength(120)]
    public string Title { get; set; } = string.Empty;
    [Required, StringLength(260)]
    public string ShortDescription { get; set; } = string.Empty;
    [StringLength(800)]
    public string FullDescription { get; set; } = string.Empty;
    [StringLength(60)]
    public string Icon { get; set; } = "truck";
    [StringLength(600)]
    public string ImageUrl { get; set; } = string.Empty;
    [Range(0, 1000)]
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public class FeatureInputViewModel
{
    public int Id { get; set; }
    [Required, StringLength(120)]
    public string Title { get; set; } = string.Empty;
    [Required, StringLength(600)]
    public string Description { get; set; } = string.Empty;
    [StringLength(60)]
    public string Icon { get; set; } = "shield";
    [Range(0, 1000)]
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public class StatisticInputViewModel
{
    public int Id { get; set; }
    [Required, StringLength(40)]
    public string Value { get; set; } = string.Empty;
    [Required, StringLength(120)]
    public string Label { get; set; } = string.Empty;
    [StringLength(60)]
    public string Icon { get; set; } = "";
    [Range(0, 1000)]
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public class QuoteStatusUpdateInputViewModel
{
    public int Id { get; set; }
    public QuoteStatus Status { get; set; }
}
