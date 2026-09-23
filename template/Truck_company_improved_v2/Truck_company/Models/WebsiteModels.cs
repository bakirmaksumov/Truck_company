using System.ComponentModel.DataAnnotations;

namespace Truck_company.Models;

public class SiteSettings
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = "Express Liner LLC";
    public string LogoText { get; set; } = "EXPRESS";
    public string LogoSubText { get; set; } = "LINER LLC";
    public string Tagline { get; set; } = "Moving freight, delivering trust";
    public string PrimaryPhone { get; set; } = "+1 (386) 8438081";
    public string Email { get; set; } = "info@expressliner.com";
    public string Address { get; set; } = "1917 James St, South Daytona FL 32119";
    public string FooterText { get; set; } = "Your freight is our priority.";
    public string CopyrightText { get; set; } = "© 2025 Express Liner LLC. All Rights Reserved.";
    public string? LogoImageUrl { get; set; }
    public string MetaTitle { get; set; } = "Express Liner LLC | Freight Transportation";
    public string MetaDescription { get; set; } = "Reliable dry van, regional and long-haul freight transportation across the United States.";
    public string MetaKeywords { get; set; } = "freight, trucking, logistics, dry van, transportation";
    public string OpenGraphTitle { get; set; } = "Moving Freight. Delivering Trust.";
    public string OpenGraphDescription { get; set; } = "Safe, reliable and on-time freight transportation.";
    public string? OpenGraphImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
}

public class HeroSection
{
    public int Id { get; set; }
    public string Title { get; set; } = "Moving Freight";
    public string HighlightTitle { get; set; } = "Delivering Trust";
    public string Description { get; set; } = "Express Liner LLC is a reliable and efficient transportation company dedicated to delivering freight safely, on time, every time.";
    public string BackgroundImageUrl { get; set; } = "https://images.unsplash.com/...";
    public string PrimaryButtonText { get; set; } = "Get a Quote";
    public string SecondaryButtonText { get; set; } = "Our Services";
    public string PrimaryButtonUrl { get; set; } = "#quote";
    public string SecondaryButtonUrl { get; set; } = "#services";
    public bool IsActive { get; set; } = true;
}

public class AboutSection
{
    public int Id { get; set; }
    public string Title { get; set; } = "About Express Liner LLC";
    public string Subtitle { get; set; } = "A trusted partner in logistics";
    public string Description { get; set; } = "At Express Liner LLC, our mission is simple – to provide dependable, efficient, and cost-effective transportation solutions.";
    public string ImageUrl { get; set; } = "https://images.unsplash.com/...";
    public bool IsActive { get; set; } = true;
}

public class Statistic
{
    public int Id { get; set; }
    public string Value { get; set; } = "10+";
    public string Label { get; set; } = "Years of Experience";
    public string Icon { get; set; } = "⚡";
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public class Service
{
    public int Id { get; set; }
    [Required]
    [StringLength(120)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(260)]
    public string ShortDescription { get; set; } = string.Empty;

    [StringLength(800)]
    public string FullDescription { get; set; } = string.Empty;

    public string Icon { get; set; } = "truck";
    public string ImageUrl { get; set; } = "";
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public class Feature
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Icon { get; set; } = "shield";
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public class ContactSettings
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = "Express Liner LLC";
    public string Address { get; set; } = "1917 James St, South Daytona FL 32119";
    public string Phone { get; set; } = "+1 (386) 8438081";
    public string Email { get; set; } = "info@expressliner.com";
    public string BusinessHours { get; set; } = "Mon-Fri: 8:00 AM – 5:00 PM";
    public string MapUrl { get; set; } = "https://maps.google.com/?q=1917+James+St+South+Daytona+FL+32119";
    public string FacebookUrl { get; set; } = "#";
    public string InstagramUrl { get; set; } = "#";
    public string LinkedInUrl { get; set; } = "#";
    public bool IsActive { get; set; } = true;
}

public class SocialLink
{
    public int Id { get; set; }
    public string Platform { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;
    public string IconCssClass { get; set; } = "fab fa-facebook-f";
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public enum QuoteStatus
{
    New,
    InProgress,
    Contacted,
    Completed,
    Archived
}

public class QuoteRequest
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string CompanyName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string PickupLocation { get; set; } = string.Empty;
    public string DeliveryLocation { get; set; } = string.Empty;
    public string FreightType { get; set; } = string.Empty;
    public string ApproximateWeight { get; set; } = string.Empty;
    public DateTime? PickupDate { get; set; }
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public QuoteStatus Status { get; set; } = QuoteStatus.New;
}

public class ContactMessage
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
