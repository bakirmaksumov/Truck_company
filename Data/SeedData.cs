using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Truck_company.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Truck_company.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider, IConfiguration configuration)
    {
        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var environment = serviceProvider.GetRequiredService<IHostEnvironment>();
        var logger = serviceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("SeedData");

        logger.LogInformation("Starting database migration.");
        try
        {
            await dbContext.Database.MigrateAsync();
            logger.LogInformation("Database migration completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogCritical(ex, "Database migration failed during application startup.");
            throw;
        }

        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        if (!await roleManager.RoleExistsAsync("Admin"))
        {
            await roleManager.CreateAsync(new IdentityRole("Admin"));
        }

        var adminEmail = configuration["Admin:Email"];
        var adminPassword = configuration["Admin:Password"];

        if (!string.IsNullOrWhiteSpace(adminEmail) && !string.IsNullOrWhiteSpace(adminPassword))
        {
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
        }
        else
        {
            if (environment.IsDevelopment())
            {
                logger.LogWarning("Admin user was not seeded because Admin:Email/Admin:Password are not configured. Set them in user secrets for development.");
            }
            else
            {
                logger.LogCritical("Admin user was not seeded because Admin:Email/Admin:Password are not configured in production.");
            }
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
                MetaTitle = "Express Liner LLC | Freight Transportation",
                MetaDescription = "Reliable dry van, regional and long-haul freight transportation across the United States.",
                MetaKeywords = "freight, trucking, logistics, dry van, transportation",
                OpenGraphTitle = "Moving Freight. Delivering Trust.",
                OpenGraphDescription = "Safe, reliable and on-time freight transportation.",
                OpenGraphImageUrl = null,
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
                BackgroundImageUrl = "/images/truck.png",
                PrimaryButtonText = "Get a Quote",
                SecondaryButtonText = "Our Services",
                PrimaryButtonUrl = "#quote",
                SecondaryButtonUrl = "#services",
                IsActive = true
            });
        }
        else
        {
            var existingHero = await dbContext.HeroSections.FirstOrDefaultAsync();
            if (existingHero != null)
            {
                existingHero.BackgroundImageUrl = "/images/truck.png";
            }
        }

        if (!await dbContext.AboutSections.AnyAsync())
        {
            dbContext.AboutSections.Add(new AboutSection
            {
                Title = "EXPRESS LINER LLC",
                Subtitle = "ABOUT US",
                Description = "At Express Liner LLC, our mission is simple – to provide dependable, efficient, and cost-effective transportation solutions. With a focus on safety, communication, and customer satisfaction, we go the extra mile to keep your business moving forward.",
                ImageUrl = "/images/truck2.png",
                IsActive = true
            });
        }
        else
        {
            var existingAbout = await dbContext.AboutSections.FirstOrDefaultAsync();
            if (existingAbout != null)
            {
                existingAbout.Title = "EXPRESS LINER LLC";
                existingAbout.Subtitle = "ABOUT US";
                existingAbout.Description = "At Express Liner LLC, our mission is simple – to provide dependable, efficient, and cost-effective transportation solutions. With a focus on safety, communication, and customer satisfaction, we go the extra mile to keep your business moving forward.";
                existingAbout.ImageUrl = "/images/truck2.png";
            }
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
                new Statistic { Value = "100%", Label = "Safety & Compliance", Icon = "🛡️", DisplayOrder = 1, IsActive = true },
                new Statistic { Value = "48", Label = "States Nationwide Coverage", Icon = "🗺️", DisplayOrder = 2, IsActive = true },
                new Statistic { Value = "24/7", Label = "Live Dispatch & Tracking", Icon = "⏱", DisplayOrder = 3, IsActive = true },
                new Statistic { Value = "100%", Label = "On-Time Commitment", Icon = "⭐", DisplayOrder = 4, IsActive = true }
            );
        }
        else
        {
            var existingStats = await dbContext.Statistics.OrderBy(s => s.DisplayOrder).ThenBy(s => s.Id).ToListAsync();
            if (existingStats.Any(s => s.Value.Contains("10+") || s.Value.Contains("50K+") || s.Value.Contains("1.2M") || s.Label.Contains("Years")))
            {
                if (existingStats.Count >= 1) { existingStats[0].Value = "100%"; existingStats[0].Label = "Safety & Compliance"; existingStats[0].Icon = "🛡️"; }
                if (existingStats.Count >= 2) { existingStats[1].Value = "48"; existingStats[1].Label = "States Nationwide Coverage"; existingStats[1].Icon = "🗺️"; }
                if (existingStats.Count >= 3) { existingStats[2].Value = "24/7"; existingStats[2].Label = "Live Dispatch & Tracking"; existingStats[2].Icon = "⏱"; }
                if (existingStats.Count >= 4) { existingStats[3].Value = "100%"; existingStats[3].Label = "On-Time Commitment"; existingStats[3].Icon = "⭐"; }
            }
        }
        await dbContext.SaveChangesAsync();

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
