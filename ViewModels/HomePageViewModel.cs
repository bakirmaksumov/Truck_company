using Truck_company.Models;
using System.ComponentModel.DataAnnotations;

namespace Truck_company.ViewModels;

public class HomePageViewModel
{
    public SiteSettings SiteSettings { get; set; } = new();
    public HeroSection HeroSection { get; set; } = new();
    public AboutSection AboutSection { get; set; } = new();
    public List<Statistic> Statistics { get; set; } = new();
    public List<Service> Services { get; set; } = new();
    public List<Feature> Features { get; set; } = new();
    public ContactSettings ContactSettings { get; set; } = new();
    public List<SocialLink> SocialLinks { get; set; } = new();
    public QuoteRequestInputViewModel QuoteForm { get; set; } = new();
    public ContactMessageInputViewModel ContactForm { get; set; } = new();
}

public class QuoteRequestInputViewModel
{
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
}

public class ContactMessageInputViewModel
{
    [Required, StringLength(120)]
    public string Name { get; set; } = string.Empty;
    [Required, EmailAddress, StringLength(160)]
    public string Email { get; set; } = string.Empty;
    [Phone, StringLength(40)]
    public string Phone { get; set; } = string.Empty;
    [Required, StringLength(1500)]
    public string Message { get; set; } = string.Empty;
}
