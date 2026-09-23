using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Truck_company.Models;

namespace Truck_company.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<SiteSettings> SiteSettings { get; set; }
    public DbSet<HeroSection> HeroSections { get; set; }
    public DbSet<AboutSection> AboutSections { get; set; }
    public DbSet<Statistic> Statistics { get; set; }
    public DbSet<Service> Services { get; set; }
    public DbSet<Feature> Features { get; set; }
    public DbSet<ContactSettings> ContactSettings { get; set; }
    public DbSet<SocialLink> SocialLinks { get; set; }
    public DbSet<QuoteRequest> QuoteRequests { get; set; }
    public DbSet<ContactMessage> ContactMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<SiteSettings>().HasData(new SiteSettings
        {
            Id = 1,
            CompanyName = "Express Liner LLC",
            LogoText = "EXPRESS",
            LogoSubText = "LINER LLC",
            Tagline = "Moving freight, delivering trust",
            PrimaryPhone = "+1 (386) 8438081",
            Email = "info@expressliner.com",
            Address = "1917 James St, South Daytona FL 32119",
            FooterText = "Your freight is our priority.",
            CopyrightText = "© 2025 Express Liner LLC. All Rights Reserved."
        });

        builder.Entity<HeroSection>().HasData(new HeroSection
        {
            Id = 1,
            Title = "Moving Freight",
            HighlightTitle = "Delivering Trust",
            Description = "Express Liner LLC is a reliable and efficient transportation company dedicated to delivering freight safely, on time, every time.",
            BackgroundImageUrl = "https://images.unsplash.com/photo-1601584115197-04ecc0da31d7?auto=format&fit=crop&w=1600&q=80",
            PrimaryButtonText = "Get a Quote",
            SecondaryButtonText = "Our Services",
            PrimaryButtonUrl = "#quote",
            SecondaryButtonUrl = "#services",
            IsActive = true
        });

        builder.Entity<AboutSection>().HasData(new AboutSection
        {
            Id = 1,
            Title = "About Express Liner LLC",
            Subtitle = "A trusted partner in logistics",
            Description = "At Express Liner LLC, our mission is simple – to provide dependable, efficient, and cost-effective transportation solutions. With a focus on safety, communication, and customer satisfaction, we go the extra mile to keep your business moving forward.",
            ImageUrl = "https://images.unsplash.com/photo-1601584115197-04ecc0da31d7?auto=format&fit=crop&w=1200&q=80",
            IsActive = true
        });

        builder.Entity<ContactSettings>().HasData(new ContactSettings
        {
            Id = 1,
            CompanyName = "Express Liner LLC",
            Address = "1917 James St, South Daytona FL 32119",
            Phone = "+1 (386) 8438081",
            Email = "info@expressliner.com",
            BusinessHours = "Mon-Fri: 8:00 AM – 5:00 PM",
            MapUrl = "https://maps.google.com/?q=1917+James+St+South+Daytona+FL+32119",
            FacebookUrl = "#",
            InstagramUrl = "#",
            LinkedInUrl = "#",
            IsActive = true
        });

        builder.Entity<Statistic>().HasData(
            new Statistic { Id = 1, Value = "10+", Label = "Years of Experience", Icon = "⏱", DisplayOrder = 1, IsActive = true },
            new Statistic { Id = 2, Value = "50K+", Label = "Successful Deliveries", Icon = "📦", DisplayOrder = 2, IsActive = true },
            new Statistic { Id = 3, Value = "98%", Label = "Satisfied Clients", Icon = "⭐", DisplayOrder = 3, IsActive = true },
            new Statistic { Id = 4, Value = "1.2M", Label = "Miles Covered", Icon = "🛣", DisplayOrder = 4, IsActive = true }
        );

        builder.Entity<Service>().HasData(
            new Service { Id = 1, Title = "Dry Van Transport", ShortDescription = "Safe, secure, and on-time delivery for all your dry van freight needs across the 48 states.", FullDescription = "We transport dry freight with dependable scheduling and secure handling from pickup to delivery.", Icon = "truck", ImageUrl = "https://images.unsplash.com/photo-1601584115197-04ecc0da31d7?auto=format&fit=crop&w=900&q=80", DisplayOrder = 1, IsActive = true },
            new Service { Id = 2, Title = "Long Haul & Regional", ShortDescription = "Reliable capacity for long haul and regional shipments with dedicated support.", FullDescription = "Flexible solutions for long-distance, regional freight moves with responsive communication and tracking.", Icon = "location", ImageUrl = "https://images.unsplash.com/photo-1517048676732-d65bc937f952?auto=format&fit=crop&w=900&q=80", DisplayOrder = 2, IsActive = true },
            new Service { Id = 3, Title = "Experienced Team", ShortDescription = "Professional drivers and logistics experts you can count on.", FullDescription = "Our team is trained to deliver efficient route planning, cargo care, and customer communication.", Icon = "users", ImageUrl = "https://images.unsplash.com/photo-1552664730-d307ca884978?auto=format&fit=crop&w=900&q=80", DisplayOrder = 3, IsActive = true },
            new Service { Id = 4, Title = "Safety First", ShortDescription = "We follow the highest safety standards to protect your freight.", FullDescription = "Safety programs and inspections are built into our daily operations to keep cargo secure and compliant.", Icon = "shield", ImageUrl = "https://images.unsplash.com/photo-1601584115197-04ecc0da31d7?auto=format&fit=crop&w=900&q=80", DisplayOrder = 4, IsActive = true }
        );

        builder.Entity<Feature>().HasData(
            new Feature { Id = 1, Title = "Reliable & On Time", Description = "We value your time and make sure your freight arrives on schedule.", Icon = "shield", DisplayOrder = 1, IsActive = true },
            new Feature { Id = 2, Title = "Experienced Team", Description = "Our team of professionals has the experience and knowledge to get the job done right.", Icon = "users", DisplayOrder = 2, IsActive = true },
            new Feature { Id = 3, Title = "Safety First", Description = "Safety is at the core of everything we do.", Icon = "check", DisplayOrder = 3, IsActive = true },
            new Feature { Id = 4, Title = "Competitive Rates", Description = "We offer quality service at fair and transparent prices.", Icon = "dollar", DisplayOrder = 4, IsActive = true }
        );

        builder.Entity<SocialLink>().HasData(
            new SocialLink { Id = 1, Platform = "Facebook", Url = "#", IconCssClass = "fab fa-facebook-f", DisplayOrder = 1, IsActive = true },
            new SocialLink { Id = 2, Platform = "Instagram", Url = "#", IconCssClass = "fab fa-instagram", DisplayOrder = 2, IsActive = true },
            new SocialLink { Id = 3, Platform = "LinkedIn", Url = "#", IconCssClass = "fab fa-linkedin-in", DisplayOrder = 3, IsActive = true }
        );
    }
}
