using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Truck_company.Models;

namespace Truck_company.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        await dbContext.Database.MigrateAsync();

        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        var adminEmail = configuration["Admin:Email"] ?? "admin@expressliner.com";
        var adminPassword = configuration["Admin:Password"] ?? "Admin123!";

        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser is null)
        {
            adminUser = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = "Administrator",
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, adminPassword);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException(string.Join("; ", result.Errors.Select(e => e.Description)));
            }
        }

        if (!await userManager.IsInRoleAsync(adminUser, "Admin"))
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }

        if (!await dbContext.SiteSettings.AnyAsync())
        {
            dbContext.SiteSettings.Add(new SiteSettings
            {
                CompanyName = "Express Liner LLC",
                LogoText = "EXPRESS",
                LogoSubText = "LINER LLC",
                Tagline = "Moving freight, delivering trust",
                PrimaryPhone = "+1 (386) 8438081",
                Email = "info@expressliner.com",
                Address = "1917 James St, South Daytona FL 32119",
                FooterText = "Your freight is our priority.",
                CopyrightText = "© 2025 Express Liner LLC. All Rights Reserved.",
                IsActive = true
            });
        }

        if (!await dbContext.HeroSections.AnyAsync())
        {
            dbContext.HeroSections.Add(new HeroSection
            {
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
        }

        if (!await dbContext.AboutSections.AnyAsync())
        {
            dbContext.AboutSections.Add(new AboutSection
            {
                Title = "About Express Liner LLC",
                Subtitle = "A trusted partner in logistics",
                Description = "At Express Liner LLC, our mission is simple – to provide dependable, efficient, and cost-effective transportation solutions. With a focus on safety, communication, and customer satisfaction, we go the extra mile to keep your business moving forward.",
                ImageUrl = "https://images.unsplash.com/photo-1601584115197-04ecc0da31d7?auto=format&fit=crop&w=1200&q=80",
                IsActive = true
            });
        }

        if (!await dbContext.ContactSettings.AnyAsync())
        {
            dbContext.ContactSettings.Add(new ContactSettings
            {
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
        }

        if (!await dbContext.Statistics.AnyAsync())
        {
            dbContext.Statistics.AddRange(
                new Statistic { Value = "10+", Label = "Years of Experience", Icon = "⏱", DisplayOrder = 1, IsActive = true },
                new Statistic { Value = "50K+", Label = "Successful Deliveries", Icon = "📦", DisplayOrder = 2, IsActive = true },
                new Statistic { Value = "98%", Label = "Satisfied Clients", Icon = "⭐", DisplayOrder = 3, IsActive = true },
                new Statistic { Value = "1.2M", Label = "Miles Covered", Icon = "🛣", DisplayOrder = 4, IsActive = true }
            );
        }

        if (!await dbContext.Services.AnyAsync())
        {
            dbContext.Services.AddRange(
                new Service { Title = "Dry Van Transport", ShortDescription = "Safe, secure, and on-time delivery for all your dry van freight needs across the 48 states.", FullDescription = "We transport dry freight with dependable scheduling and secure handling from pickup to delivery.", Icon = "truck", ImageUrl = "https://images.unsplash.com/photo-1601584115197-04ecc0da31d7?auto=format&fit=crop&w=900&q=80", DisplayOrder = 1, IsActive = true },
                new Service { Title = "Long Haul & Regional", ShortDescription = "Reliable capacity for long haul and regional shipments with dedicated support.", FullDescription = "Flexible solutions for long-distance, regional freight moves with responsive communication and tracking.", Icon = "location", ImageUrl = "https://images.unsplash.com/photo-1517048676732-d65bc937f952?auto=format&fit=crop&w=900&q=80", DisplayOrder = 2, IsActive = true },
                new Service { Title = "Experienced Team", ShortDescription = "Professional drivers and logistics experts you can count on.", FullDescription = "Our team is trained to deliver efficient route planning, cargo care, and customer communication.", Icon = "users", ImageUrl = "https://images.unsplash.com/photo-1552664730-d307ca884978?auto=format&fit=crop&w=900&q=80", DisplayOrder = 3, IsActive = true },
                new Service { Title = "Safety First", ShortDescription = "We follow the highest safety standards to protect your freight.", FullDescription = "Safety programs and inspections are built into our daily operations to keep cargo secure and compliant.", Icon = "shield", ImageUrl = "https://images.unsplash.com/photo-1601584115197-04ecc0da31d7?auto=format&fit=crop&w=900&q=80", DisplayOrder = 4, IsActive = true }
            );
        }

        if (!await dbContext.Features.AnyAsync())
        {
            dbContext.Features.AddRange(
                new Feature { Title = "Reliable & On Time", Description = "We value your time and make sure your freight arrives on schedule.", Icon = "shield", DisplayOrder = 1, IsActive = true },
                new Feature { Title = "Experienced Team", Description = "Our team of professionals has the experience and knowledge to get the job done right.", Icon = "users", DisplayOrder = 2, IsActive = true },
                new Feature { Title = "Safety First", Description = "Safety is at the core of everything we do.", Icon = "check", DisplayOrder = 3, IsActive = true },
                new Feature { Title = "Competitive Rates", Description = "We offer quality service at fair and transparent prices.", Icon = "dollar", DisplayOrder = 4, IsActive = true }
            );
        }

        if (!await dbContext.SocialLinks.AnyAsync())
        {
            dbContext.SocialLinks.AddRange(
                new SocialLink { Platform = "Facebook", Url = "#", IconCssClass = "fab fa-facebook-f", DisplayOrder = 1, IsActive = true },
                new SocialLink { Platform = "Instagram", Url = "#", IconCssClass = "fab fa-instagram", DisplayOrder = 2, IsActive = true },
                new SocialLink { Platform = "LinkedIn", Url = "#", IconCssClass = "fab fa-linkedin-in", DisplayOrder = 3, IsActive = true }
            );
        }

        await dbContext.SaveChangesAsync();
    }
}
