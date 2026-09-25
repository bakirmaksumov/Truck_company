using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Truck_company.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddAboutSectionPillars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MissionDescription",
                table: "AboutSections",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MissionTitle",
                table: "AboutSections",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ValuesDescription",
                table: "AboutSections",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ValuesTitle",
                table: "AboutSections",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VisionDescription",
                table: "AboutSections",
                type: "nvarchar(400)",
                maxLength: 400,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "VisionTitle",
                table: "AboutSections",
                type: "nvarchar(80)",
                maxLength: 80,
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AboutSections",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageUrl", "MissionDescription", "MissionTitle", "Subtitle", "Title", "ValuesDescription", "ValuesTitle", "VisionDescription", "VisionTitle" },
                values: new object[] { "/images/photo_6_2026-08-23_21-23-06.jpg", "Deliver freight safely, on time, every time.", "Our Mission", "ABOUT US", "EXPRESS LINER LLC", "Safety, reliability, and excellence in every load we haul.", "Our Values", "To be a trusted leader in the transportation industry.", "Our Vision" });

            migrationBuilder.UpdateData(
                table: "Statistics",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Icon", "Label", "Value" },
                values: new object[] { "🛡️", "Safety & Compliance", "100%" });

            migrationBuilder.UpdateData(
                table: "Statistics",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Icon", "Label", "Value" },
                values: new object[] { "🗺️", "States Nationwide Coverage", "48" });

            migrationBuilder.UpdateData(
                table: "Statistics",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Icon", "Label", "Value" },
                values: new object[] { "⏱", "Live Dispatch & Tracking", "24/7" });

            migrationBuilder.UpdateData(
                table: "Statistics",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Icon", "Label", "Value" },
                values: new object[] { "⭐", "On-Time Commitment", "100%" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MissionDescription",
                table: "AboutSections");

            migrationBuilder.DropColumn(
                name: "MissionTitle",
                table: "AboutSections");

            migrationBuilder.DropColumn(
                name: "ValuesDescription",
                table: "AboutSections");

            migrationBuilder.DropColumn(
                name: "ValuesTitle",
                table: "AboutSections");

            migrationBuilder.DropColumn(
                name: "VisionDescription",
                table: "AboutSections");

            migrationBuilder.DropColumn(
                name: "VisionTitle",
                table: "AboutSections");

            migrationBuilder.UpdateData(
                table: "AboutSections",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ImageUrl", "Subtitle", "Title" },
                values: new object[] { "https://images.unsplash.com/photo-1601584115197-04ecc0da31d7?auto=format&fit=crop&w=1200&q=80", "A trusted partner in logistics", "About Express Liner LLC" });

            migrationBuilder.UpdateData(
                table: "Statistics",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Icon", "Label", "Value" },
                values: new object[] { "⏱", "Years of Experience", "10+" });

            migrationBuilder.UpdateData(
                table: "Statistics",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Icon", "Label", "Value" },
                values: new object[] { "📦", "Successful Deliveries", "50K+" });

            migrationBuilder.UpdateData(
                table: "Statistics",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "Icon", "Label", "Value" },
                values: new object[] { "⭐", "Satisfied Clients", "98%" });

            migrationBuilder.UpdateData(
                table: "Statistics",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "Icon", "Label", "Value" },
                values: new object[] { "🛣", "Miles Covered", "1.2M" });
        }
    }
}
