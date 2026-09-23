using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Truck_company.Data;

#nullable disable

namespace Truck_company.Data.Migrations;

[DbContext(typeof(ApplicationDbContext))]
[Migration("20260824090000_AddSeoFields")]
public partial class AddSeoFields : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(name: "MetaTitle", table: "SiteSettings", type: "TEXT", nullable: false, defaultValue: "Express Liner LLC | Freight Transportation");
        migrationBuilder.AddColumn<string>(name: "MetaDescription", table: "SiteSettings", type: "TEXT", nullable: false, defaultValue: "Reliable dry van, regional and long-haul freight transportation across the United States.");
        migrationBuilder.AddColumn<string>(name: "MetaKeywords", table: "SiteSettings", type: "TEXT", nullable: false, defaultValue: "freight, trucking, logistics, dry van, transportation");
        migrationBuilder.AddColumn<string>(name: "OpenGraphTitle", table: "SiteSettings", type: "TEXT", nullable: false, defaultValue: "Moving Freight. Delivering Trust.");
        migrationBuilder.AddColumn<string>(name: "OpenGraphDescription", table: "SiteSettings", type: "TEXT", nullable: false, defaultValue: "Safe, reliable and on-time freight transportation.");
        migrationBuilder.AddColumn<string>(name: "OpenGraphImageUrl", table: "SiteSettings", type: "TEXT", nullable: true);
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(name: "MetaTitle", table: "SiteSettings");
        migrationBuilder.DropColumn(name: "MetaDescription", table: "SiteSettings");
        migrationBuilder.DropColumn(name: "MetaKeywords", table: "SiteSettings");
        migrationBuilder.DropColumn(name: "OpenGraphTitle", table: "SiteSettings");
        migrationBuilder.DropColumn(name: "OpenGraphDescription", table: "SiteSettings");
        migrationBuilder.DropColumn(name: "OpenGraphImageUrl", table: "SiteSettings");
    }
}
