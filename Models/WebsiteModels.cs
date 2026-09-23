using System.ComponentModel.DataAnnotations;

namespace Truck_company.Models;

public class SiteSettings
{
    public int Id { get; set; }
    [Required, StringLength(160)]
    public string CompanyName { get; set; } = "Express Liner LLC";
    [Required, StringLength(60)]
    public string LogoText { get; set; } = "EXPRESS";
    [Required, StringLength(60)]
    public string LogoSubText { get; set; } = "LINER LLC";
    [Required, StringLength(180)]
    public string Tagline { get; set; } = "Moving freight, delivering trust";
    [Required, Phone, StringLength(40)]
    public string PrimaryPhone { get; set; } = "+1 (386) 8438081";
    [Required, EmailAddress, StringLength(160)]
    public string Email { get; set; } = "info@expressliner.com";
    [Required, StringLength(220)]
    public string Address { get; set; } = "1917 James St, South Daytona FL 32119";
    [Required, StringLength(220)]
    public string FooterText { get; set; } = "Your freight is our priority.";
    [Required, StringLength(220)]
    public string CopyrightText { get; set; } = "© 2025 Express Liner LLC. All Rights Reserved.";
    public string? LogoImageUrl { get; set; }
    [Required, StringLength(160)]
    public string MetaTitle { get; set; } = "Express Liner LLC | Freight Transportation";
    [Required, StringLength(320)]
    public string MetaDescription { get; set; } = "Reliable dry van, regional and long-haul freight transportation across the United States.";
    [Required, StringLength(320)]
    public string MetaKeywords { get; set; } = "freight, trucking, logistics, dry van, transportation";
    [Required, StringLength(160)]
    public string OpenGraphTitle { get; set; } = "Moving Freight. Delivering Trust.";
    [Required, StringLength(320)]
    public string OpenGraphDescription { get; set; } = "Safe, reliable and on-time freight transportation.";
    public string? OpenGraphImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
}

public class HeroSection
{
    public int Id { get; set; }
    [Required, StringLength(120)]
    public string Title { get; set; } = "Moving Freight";
    [Required, StringLength(120)]
    public string HighlightTitle { get; set; } = "Delivering Trust";
    [Required, StringLength(1200)]
    public string Description { get; set; } = "Express Liner LLC is a reliable and efficient transportation company dedicated to delivering freight safely, on time, every time.";
    [Required, StringLength(600)]
    public string BackgroundImageUrl { get; set; } = "https://images.unsplash.com/...";
    [Required, StringLength(60)]
    public string PrimaryButtonText { get; set; } = "Get a Quote";
    [Required, StringLength(60)]
    public string SecondaryButtonText { get; set; } = "Our Services";
    [Required, StringLength(250)]
    public string PrimaryButtonUrl { get; set; } = "#quote";
    [Required, StringLength(250)]
    public string SecondaryButtonUrl { get; set; } = "#services";
    public bool IsActive { get; set; } = true;
}

public class AboutSection
{
    public int Id { get; set; }
    [Required, StringLength(160)]
    public string Title { get; set; } = "About Express Liner LLC";
    [Required, StringLength(220)]
    public string Subtitle { get; set; } = "A trusted partner in logistics";
    [Required, StringLength(2000)]
    public string Description { get; set; } = "At Express Liner LLC, our mission is simple – to provide dependable, efficient, and cost-effective transportation solutions.";
    [Required, StringLength(600)]
    public string ImageUrl { get; set; } = "https://images.unsplash.com/...";
    public bool IsActive { get; set; } = true;
}

public class Statistic
{
    public int Id { get; set; }
    [Required, StringLength(40)]
    public string Value { get; set; } = "10+";
    [Required, StringLength(120)]
    public string Label { get; set; } = "Years of Experience";
    [StringLength(60)]
    public string Icon { get; set; } = "⚡";
    [Range(0, 1000)]
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

    [StringLength(60)]
    public string Icon { get; set; } = "truck";
    [StringLength(600)]
    public string ImageUrl { get; set; } = "";
    [Range(0, 1000)]
    public int DisplayOrder { get; set; }
    public bool IsActive { get; set; } = true;
}

public class Feature
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

public class ContactSettings
{
    public int Id { get; set; }
    [Required, StringLength(160)]
    public string CompanyName { get; set; } = "Express Liner LLC";
    [Required, StringLength(220)]
    public string Address { get; set; } = "1917 James St, South Daytona FL 32119";
    [Required, Phone, StringLength(40)]
    public string Phone { get; set; } = "+1 (386) 8438081";
    [Required, EmailAddress, StringLength(160)]
    public string Email { get; set; } = "info@expressliner.com";
    [Required, StringLength(120)]
    public string BusinessHours { get; set; } = "Mon-Fri: 8:00 AM – 5:00 PM";
    [Required, StringLength(700)]
    public string MapUrl { get; set; } = "https://maps.google.com/?q=1917+James+St+South+Daytona+FL+32119";
    [StringLength(250)]
    public string FacebookUrl { get; set; } = "#";
    [StringLength(250)]
    public string InstagramUrl { get; set; } = "#";
    [StringLength(250)]
    public string LinkedInUrl { get; set; } = "#";
    public bool IsActive { get; set; } = true;
}

public class SocialLink
{
    public int Id { get; set; }
    [Required, StringLength(80)]
    public string Platform { get; set; } = string.Empty;
    [Required, StringLength(250)]
    public string Url { get; set; } = string.Empty;
    [StringLength(120)]
    public string IconCssClass { get; set; } = "fab fa-facebook-f";
    [Range(0, 1000)]
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
    [Required, StringLength(120)]
    public string FullName { get; set; } = string.Empty;
    [StringLength(120)]
    public string CompanyName { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(160)]
    public string Email { get; set; } = string.Empty;
    [Required, Phone, StringLength(40)]
    public string Phone { get; set; } = string.Empty;
    [Required, StringLength(180)]
    public string PickupLocation { get; set; } = string.Empty;
    [Required, StringLength(180)]
    public string DeliveryLocation { get; set; } = string.Empty;
    [Required, StringLength(100)]
    public string FreightType { get; set; } = string.Empty;
    [StringLength(80)]
    public string ApproximateWeight { get; set; } = string.Empty;
    [DataType(DataType.Date)]
    public DateTime? PickupDate { get; set; }
    [StringLength(1500)]
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public QuoteStatus Status { get; set; } = QuoteStatus.New;
}

public class ContactMessage
{
    public int Id { get; set; }
    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(160)]
    public string Email { get; set; } = string.Empty;
    [Phone, StringLength(40)]
    public string Phone { get; set; } = string.Empty;
    [Required, StringLength(1500)]
    public string Message { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; }
}
