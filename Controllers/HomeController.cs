using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truck_company.Data;
using Truck_company.Models;
using Truck_company.Services;
using Truck_company.ViewModels;

namespace Truck_company.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HomeController> _logger;
    private readonly IEmailSender _emailSender;

    public HomeController(ApplicationDbContext context, ILogger<HomeController> logger, IEmailSender emailSender)
    {
        _context = context;
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task<IActionResult> Index()
    {
        var model = await BuildHomePageViewModel();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitQuote([Bind(Prefix = "QuoteForm")] QuoteRequestInputViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var page = await BuildHomePageViewModel();
            page.QuoteForm = model;
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return View("Index", page);
        }

        _context.QuoteRequests.Add(new QuoteRequest
        {
            FullName = model.FullName,
            CompanyName = model.CompanyName,
            Email = model.Email,
            Phone = model.Phone,
            PickupLocation = model.PickupLocation,
            DeliveryLocation = model.DeliveryLocation,
            FreightType = model.FreightType,
            ApproximateWeight = model.ApproximateWeight,
            PickupDate = model.PickupDate,
            Message = model.Message,
            Status = QuoteStatus.New,
            CreatedAt = DateTime.UtcNow
        });

        await _context.SaveChangesAsync();
        try
        {
            await _emailSender.SendAsync(
                $"New quote request from {model.FullName}",
                $"<h2>New quote request</h2><p><strong>Name:</strong> {HtmlEncode(model.FullName)}</p><p><strong>Company:</strong> {HtmlEncode(model.CompanyName)}</p><p><strong>Email:</strong> {HtmlEncode(model.Email)}</p><p><strong>Phone:</strong> {HtmlEncode(model.Phone)}</p><p><strong>Route:</strong> {HtmlEncode(model.PickupLocation)} to {HtmlEncode(model.DeliveryLocation)}</p><p><strong>Freight:</strong> {HtmlEncode(model.FreightType)}</p><p><strong>Weight:</strong> {HtmlEncode(model.ApproximateWeight)}</p><p><strong>Pickup date:</strong> {model.PickupDate:yyyy-MM-dd}</p><p><strong>Message:</strong><br>{HtmlEncode(model.Message)}</p>",
                model.Email,
                HttpContext.RequestAborted);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Quote request was saved, but email notification failed.");
        }
        TempData["SuccessMessage"] = "Your quote request has been submitted successfully. Our team will contact you shortly.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> SubmitContact([Bind(Prefix = "ContactForm")] ContactMessageInputViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var page = await BuildHomePageViewModel();
            page.ContactForm = model;
            Response.StatusCode = StatusCodes.Status400BadRequest;
            return View("Index", page);
        }

        _context.ContactMessages.Add(new ContactMessage
        {
            Name = model.Name,
            Email = model.Email,
            Phone = model.Phone,
            Message = model.Message,
            CreatedAt = DateTime.UtcNow
        });

        await _context.SaveChangesAsync();
        try
        {
            await _emailSender.SendAsync(
                $"New contact message from {model.Name}",
                $"<h2>New contact message</h2><p><strong>Name:</strong> {HtmlEncode(model.Name)}</p><p><strong>Email:</strong> {HtmlEncode(model.Email)}</p><p><strong>Phone:</strong> {HtmlEncode(model.Phone)}</p><p><strong>Message:</strong><br>{HtmlEncode(model.Message)}</p>",
                model.Email,
                HttpContext.RequestAborted);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Contact message was saved, but email notification failed.");
        }
        TempData["SuccessMessage"] = "Your message was sent successfully. We will be in touch soon.";
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    private static string HtmlEncode(string? value) =>
        System.Net.WebUtility.HtmlEncode(value ?? string.Empty);

    [HttpGet("/sitemap.xml")]
    public IActionResult Sitemap()
    {
        var url = $"{Request.Scheme}://{Request.Host}/";
        var xml = $"<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\"><url><loc>{System.Security.SecurityElement.Escape(url)}</loc><changefreq>weekly</changefreq><priority>1.0</priority></url></urlset>";
        return Content(xml, "application/xml");
    }

    private async Task<HomePageViewModel> BuildHomePageViewModel()
    {
        var siteSettings = await _context.SiteSettings.OrderBy(x => x.Id).FirstOrDefaultAsync() ?? new SiteSettings();
        var hero = await _context.HeroSections.OrderBy(x => x.Id).FirstOrDefaultAsync() ?? new HeroSection();
        var about = await _context.AboutSections.OrderBy(x => x.Id).FirstOrDefaultAsync() ?? new AboutSection();
        var contact = await _context.ContactSettings.OrderBy(x => x.Id).FirstOrDefaultAsync() ?? new ContactSettings();

        return new HomePageViewModel
        {
            SiteSettings = siteSettings,
            HeroSection = hero,
            AboutSection = about,
            ContactSettings = contact,
            Statistics = await _context.Statistics.Where(x => x.IsActive).OrderBy(x => x.DisplayOrder).ToListAsync(),
            Services = await _context.Services.Where(x => x.IsActive).OrderBy(x => x.DisplayOrder).ToListAsync(),
            Features = await _context.Features.Where(x => x.IsActive).OrderBy(x => x.DisplayOrder).ToListAsync(),
            SocialLinks = await _context.SocialLinks.Where(x => x.IsActive).OrderBy(x => x.DisplayOrder).ToListAsync(),
            QuoteForm = new QuoteRequestInputViewModel(),
            ContactForm = new ContactMessageInputViewModel()
        };
    }
}
