using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Truck_company.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AboutSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Subtitle = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AboutSections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactMessages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactMessages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContactSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    BusinessHours = table.Column<string>(type: "TEXT", nullable: false),
                    MapUrl = table.Column<string>(type: "TEXT", nullable: false),
                    FacebookUrl = table.Column<string>(type: "TEXT", nullable: false),
                    InstagramUrl = table.Column<string>(type: "TEXT", nullable: false),
                    LinkedInUrl = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeroSections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", nullable: false),
                    HighlightTitle = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    BackgroundImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    PrimaryButtonText = table.Column<string>(type: "TEXT", nullable: false),
                    SecondaryButtonText = table.Column<string>(type: "TEXT", nullable: false),
                    PrimaryButtonUrl = table.Column<string>(type: "TEXT", nullable: false),
                    SecondaryButtonUrl = table.Column<string>(type: "TEXT", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroSections", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuoteRequests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Phone = table.Column<string>(type: "TEXT", nullable: false),
                    PickupLocation = table.Column<string>(type: "TEXT", nullable: false),
                    DeliveryLocation = table.Column<string>(type: "TEXT", nullable: false),
                    FreightType = table.Column<string>(type: "TEXT", nullable: false),
                    ApproximateWeight = table.Column<string>(type: "TEXT", nullable: false),
                    PickupDate = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Message = table.Column<string>(type: "TEXT", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuoteRequests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                    ShortDescription = table.Column<string>(type: "TEXT", maxLength: 260, nullable: false),
                    FullDescription = table.Column<string>(type: "TEXT", maxLength: 800, nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SiteSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CompanyName = table.Column<string>(type: "TEXT", nullable: false),
                    LogoText = table.Column<string>(type: "TEXT", nullable: false),
                    LogoSubText = table.Column<string>(type: "TEXT", nullable: false),
                    Tagline = table.Column<string>(type: "TEXT", nullable: false),
                    PrimaryPhone = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    FooterText = table.Column<string>(type: "TEXT", nullable: false),
                    CopyrightText = table.Column<string>(type: "TEXT", nullable: false),
                    LogoImageUrl = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SiteSettings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SocialLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Platform = table.Column<string>(type: "TEXT", nullable: false),
                    Url = table.Column<string>(type: "TEXT", nullable: false),
                    IconCssClass = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SocialLinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statistics",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<string>(type: "TEXT", nullable: false),
                    Label = table.Column<string>(type: "TEXT", nullable: false),
                    Icon = table.Column<string>(type: "TEXT", nullable: false),
                    DisplayOrder = table.Column<int>(type: "INTEGER", nullable: false),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statistics", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "AboutSections",
                columns: new[] { "Id", "Description", "ImageUrl", "IsActive", "Subtitle", "Title" },
                values: new object[] { 1, "At Express Liner LLC, our mission is simple – to provide dependable, efficient, and cost-effective transportation solutions. With a focus on safety, communication, and customer satisfaction, we go the extra mile to keep your business moving forward.", "https://images.unsplash.com/photo-1601584115197-04ecc0da31d7?auto=format&fit=crop&w=1200&q=80", true, "A trusted partner in logistics", "About Express Liner LLC" });

            migrationBuilder.InsertData(
                table: "ContactSettings",
                columns: new[] { "Id", "Address", "BusinessHours", "CompanyName", "Email", "FacebookUrl", "InstagramUrl", "IsActive", "LinkedInUrl", "MapUrl", "Phone" },
                values: new object[] { 1, "1917 James St, South Daytona FL 32119", "Mon-Fri: 8:00 AM – 5:00 PM", "Express Liner LLC", "info@expressliner.com", "#", "#", true, "#", "https://maps.google.com/?q=1917+James+St+South+Daytona+FL+32119", "+1 (386) 8438081" });

            migrationBuilder.InsertData(
                table: "Features",
                columns: new[] { "Id", "Description", "DisplayOrder", "Icon", "IsActive", "Title" },
                values: new object[,]
                {
                    { 1, "We value your time and make sure your freight arrives on schedule.", 1, "shield", true, "Reliable & On Time" },
                    { 2, "Our team of professionals has the experience and knowledge to get the job done right.", 2, "users", true, "Experienced Team" },
                    { 3, "Safety is at the core of everything we do.", 3, "check", true, "Safety First" },
                    { 4, "We offer quality service at fair and transparent prices.", 4, "dollar", true, "Competitive Rates" }
                });

            migrationBuilder.InsertData(
                table: "HeroSections",
                columns: new[] { "Id", "BackgroundImageUrl", "Description", "HighlightTitle", "IsActive", "PrimaryButtonText", "PrimaryButtonUrl", "SecondaryButtonText", "SecondaryButtonUrl", "Title" },
                values: new object[] { 1, "https://images.unsplash.com/photo-1601584115197-04ecc0da31d7?auto=format&fit=crop&w=1600&q=80", "Express Liner LLC is a reliable and efficient transportation company dedicated to delivering freight safely, on time, every time.", "Delivering Trust", true, "Get a Quote", "#quote", "Our Services", "#services", "Moving Freight" });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "DisplayOrder", "FullDescription", "Icon", "ImageUrl", "IsActive", "ShortDescription", "Title" },
                values: new object[,]
                {
                    { 1, 1, "We transport dry freight with dependable scheduling and secure handling from pickup to delivery.", "truck", "https://images.unsplash.com/photo-1601584115197-04ecc0da31d7?auto=format&fit=crop&w=900&q=80", true, "Safe, secure, and on-time delivery for all your dry van freight needs across the 48 states.", "Dry Van Transport" },
                    { 2, 2, "Flexible solutions for long-distance, regional freight moves with responsive communication and tracking.", "location", "https://images.unsplash.com/photo-1517048676732-d65bc937f952?auto=format&fit=crop&w=900&q=80", true, "Reliable capacity for long haul and regional shipments with dedicated support.", "Long Haul & Regional" },
                    { 3, 3, "Our team is trained to deliver efficient route planning, cargo care, and customer communication.", "users", "https://images.unsplash.com/photo-1552664730-d307ca884978?auto=format&fit=crop&w=900&q=80", true, "Professional drivers and logistics experts you can count on.", "Experienced Team" },
                    { 4, 4, "Safety programs and inspections are built into our daily operations to keep cargo secure and compliant.", "shield", "https://images.unsplash.com/photo-1601584115197-04ecc0da31d7?auto=format&fit=crop&w=900&q=80", true, "We follow the highest safety standards to protect your freight.", "Safety First" }
                });

            migrationBuilder.InsertData(
                table: "SiteSettings",
                columns: new[] { "Id", "Address", "CompanyName", "CopyrightText", "Email", "FooterText", "IsActive", "LogoImageUrl", "LogoSubText", "LogoText", "PrimaryPhone", "Tagline" },
                values: new object[] { 1, "1917 James St, South Daytona FL 32119", "Express Liner LLC", "© 2025 Express Liner LLC. All Rights Reserved.", "info@expressliner.com", "Your freight is our priority.", true, null, "LINER LLC", "EXPRESS", "+1 (386) 8438081", "Moving freight, delivering trust" });

            migrationBuilder.InsertData(
                table: "SocialLinks",
                columns: new[] { "Id", "DisplayOrder", "IconCssClass", "IsActive", "Platform", "Url" },
                values: new object[,]
                {
                    { 1, 1, "fab fa-facebook-f", true, "Facebook", "#" },
                    { 2, 2, "fab fa-instagram", true, "Instagram", "#" },
                    { 3, 3, "fab fa-linkedin-in", true, "LinkedIn", "#" }
                });

            migrationBuilder.InsertData(
                table: "Statistics",
                columns: new[] { "Id", "DisplayOrder", "Icon", "IsActive", "Label", "Value" },
                values: new object[,]
                {
                    { 1, 1, "⏱", true, "Years of Experience", "10+" },
                    { 2, 2, "📦", true, "Successful Deliveries", "50K+" },
                    { 3, 3, "⭐", true, "Satisfied Clients", "98%" },
                    { 4, 4, "🛣", true, "Miles Covered", "1.2M" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AboutSections");

            migrationBuilder.DropTable(
                name: "ContactMessages");

            migrationBuilder.DropTable(
                name: "ContactSettings");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "HeroSections");

            migrationBuilder.DropTable(
                name: "QuoteRequests");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "SiteSettings");

            migrationBuilder.DropTable(
                name: "SocialLinks");

            migrationBuilder.DropTable(
                name: "Statistics");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "AspNetUsers");
        }
    }
}
