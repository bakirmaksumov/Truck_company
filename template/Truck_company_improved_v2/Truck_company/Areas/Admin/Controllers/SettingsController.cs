using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truck_company.Data;
using Truck_company.Models;
using Truck_company.ViewModels;
using Truck_company.Services;

namespace Truck_company.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class SettingsController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IFileService _files;

    public SettingsController(ApplicationDbContext context, IFileService files)
    {
        _context = context;
        _files = files;
    }

    public async Task<IActionResult> Index()
    {
        var model = new AdminSettingsViewModel
        {
            SiteSettings = await _context.SiteSettings.FirstOrDefaultAsync() ?? new SiteSettings(),
            HeroSection = await _context.HeroSections.FirstOrDefaultAsync() ?? new HeroSection(),
            AboutSection = await _context.AboutSections.FirstOrDefaultAsync() ?? new AboutSection(),
            ContactSettings = await _context.ContactSettings.FirstOrDefaultAsync() ?? new ContactSettings()
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(AdminSettingsViewModel model, IFormFile? logoFile, IFormFile? heroFile, IFormFile? aboutFile, IFormFile? ogFile)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var existingSite = await _context.SiteSettings.FirstOrDefaultAsync() ?? new SiteSettings();
        var existingHero = await _context.HeroSections.FirstOrDefaultAsync() ?? new HeroSection();
        var existingAbout = await _context.AboutSections.FirstOrDefaultAsync() ?? new AboutSection();
        var existingContact = await _context.ContactSettings.FirstOrDefaultAsync() ?? new ContactSettings();

        existingSite.CompanyName = model.SiteSettings.CompanyName;
        existingSite.LogoText = model.SiteSettings.LogoText;
        existingSite.LogoSubText = model.SiteSettings.LogoSubText;
        existingSite.Tagline = model.SiteSettings.Tagline;
        existingSite.PrimaryPhone = model.SiteSettings.PrimaryPhone;
        existingSite.Email = model.SiteSettings.Email;
        existingSite.Address = model.SiteSettings.Address;
        existingSite.FooterText = model.SiteSettings.FooterText;
        existingSite.CopyrightText = model.SiteSettings.CopyrightText;
        existingSite.MetaTitle = model.SiteSettings.MetaTitle;
        existingSite.MetaDescription = model.SiteSettings.MetaDescription;
        existingSite.MetaKeywords = model.SiteSettings.MetaKeywords;
        existingSite.OpenGraphTitle = model.SiteSettings.OpenGraphTitle;
        existingSite.OpenGraphDescription = model.SiteSettings.OpenGraphDescription;

        existingHero.Title = model.HeroSection.Title;
        existingHero.HighlightTitle = model.HeroSection.HighlightTitle;
        existingHero.Description = model.HeroSection.Description;
        existingHero.PrimaryButtonText = model.HeroSection.PrimaryButtonText;
        existingHero.PrimaryButtonUrl = model.HeroSection.PrimaryButtonUrl;
        existingHero.SecondaryButtonText = model.HeroSection.SecondaryButtonText;
        existingHero.SecondaryButtonUrl = model.HeroSection.SecondaryButtonUrl;

        existingAbout.Title = model.AboutSection.Title;
        existingAbout.Subtitle = model.AboutSection.Subtitle;
        existingAbout.Description = model.AboutSection.Description;

        existingContact.CompanyName = model.ContactSettings.CompanyName;
        existingContact.Phone = model.ContactSettings.Phone;
        existingContact.Email = model.ContactSettings.Email;
        existingContact.Address = model.ContactSettings.Address;
        existingContact.BusinessHours = model.ContactSettings.BusinessHours;
        existingContact.MapUrl = model.ContactSettings.MapUrl;
        existingContact.FacebookUrl = model.ContactSettings.FacebookUrl;
        existingContact.InstagramUrl = model.ContactSettings.InstagramUrl;
        existingContact.LinkedInUrl = model.ContactSettings.LinkedInUrl;

        try
        {
            var logo = await _files.SaveImageAsync(logoFile);
            var hero = await _files.SaveImageAsync(heroFile);
            var about = await _files.SaveImageAsync(aboutFile);
            var og = await _files.SaveImageAsync(ogFile);
            if (logo is not null) { _files.DeleteImage(existingSite.LogoImageUrl); existingSite.LogoImageUrl = logo; }
            if (hero is not null) { _files.DeleteImage(existingHero.BackgroundImageUrl); existingHero.BackgroundImageUrl = hero; }
            else if (!string.IsNullOrWhiteSpace(model.HeroSection.BackgroundImageUrl)) existingHero.BackgroundImageUrl = model.HeroSection.BackgroundImageUrl;
            if (about is not null) { _files.DeleteImage(existingAbout.ImageUrl); existingAbout.ImageUrl = about; }
            else if (!string.IsNullOrWhiteSpace(model.AboutSection.ImageUrl)) existingAbout.ImageUrl = model.AboutSection.ImageUrl;
            if (og is not null) { _files.DeleteImage(existingSite.OpenGraphImageUrl); existingSite.OpenGraphImageUrl = og; }
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(string.Empty, ex.Message);
            return View(model);
        }

        if (existingSite.Id == 0) _context.SiteSettings.Add(existingSite);
        if (existingHero.Id == 0) _context.HeroSections.Add(existingHero);
        if (existingAbout.Id == 0) _context.AboutSections.Add(existingAbout);
        if (existingContact.Id == 0) _context.ContactSettings.Add(existingContact);

        await _context.SaveChangesAsync();
        TempData["SuccessMessage"] = "Website settings were updated successfully.";
        return RedirectToAction(nameof(Index));
    }
}
