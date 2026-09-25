using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truck_company.Data;
using Truck_company.Models;
using Truck_company.Services;
using Truck_company.ViewModels;

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
        var site = await _context.SiteSettings.OrderBy(x => x.Id).FirstOrDefaultAsync() ?? new SiteSettings();
        var hero = await _context.HeroSections.OrderBy(x => x.Id).FirstOrDefaultAsync() ?? new HeroSection();
        var about = await _context.AboutSections.OrderBy(x => x.Id).FirstOrDefaultAsync() ?? new AboutSection();
        var contact = await _context.ContactSettings.OrderBy(x => x.Id).FirstOrDefaultAsync() ?? new ContactSettings();

        var model = new AdminSettingsInputViewModel
        {
            SiteSettings = new SiteSettingsInputViewModel
            {
                CompanyName = site.CompanyName,
                LogoText = site.LogoText,
                LogoSubText = site.LogoSubText,
                Tagline = site.Tagline,
                PrimaryPhone = site.PrimaryPhone,
                Email = site.Email,
                Address = site.Address,
                FooterText = site.FooterText,
                CopyrightText = site.CopyrightText,
                MetaTitle = site.MetaTitle,
                MetaDescription = site.MetaDescription,
                MetaKeywords = site.MetaKeywords,
                OpenGraphTitle = site.OpenGraphTitle,
                OpenGraphDescription = site.OpenGraphDescription
            },
            HeroSection = new HeroSectionInputViewModel
            {
                Title = hero.Title,
                HighlightTitle = hero.HighlightTitle,
                Description = hero.Description,
                BackgroundImageUrl = hero.BackgroundImageUrl,
                PrimaryButtonText = hero.PrimaryButtonText,
                PrimaryButtonUrl = hero.PrimaryButtonUrl,
                SecondaryButtonText = hero.SecondaryButtonText,
                SecondaryButtonUrl = hero.SecondaryButtonUrl
            },
            AboutSection = new AboutSectionInputViewModel
            {
                Title = about.Title,
                Subtitle = about.Subtitle,
                Description = about.Description,
                ImageUrl = about.ImageUrl,
                MissionTitle = about.MissionTitle,
                MissionDescription = about.MissionDescription,
                VisionTitle = about.VisionTitle,
                VisionDescription = about.VisionDescription,
                ValuesTitle = about.ValuesTitle,
                ValuesDescription = about.ValuesDescription,
                IsActive = about.IsActive
            },
            ContactSettings = new ContactSettingsInputViewModel
            {
                CompanyName = contact.CompanyName,
                Address = contact.Address,
                Phone = contact.Phone,
                Email = contact.Email,
                BusinessHours = contact.BusinessHours,
                MapUrl = contact.MapUrl,
                FacebookUrl = contact.FacebookUrl,
                InstagramUrl = contact.InstagramUrl,
                LinkedInUrl = contact.LinkedInUrl
            },
            CurrentLogoUrl = site.LogoImageUrl,
            CurrentHeroImageUrl = hero.BackgroundImageUrl,
            CurrentAboutImageUrl = about.ImageUrl,
            CurrentOpenGraphImageUrl = site.OpenGraphImageUrl
        };

        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(AdminSettingsInputViewModel model, IFormFile? logoFile, IFormFile? heroFile, IFormFile? aboutFile, IFormFile? ogFile)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var existingSite = await _context.SiteSettings.OrderBy(x => x.Id).FirstOrDefaultAsync() ?? new SiteSettings();
        var existingHero = await _context.HeroSections.OrderBy(x => x.Id).FirstOrDefaultAsync() ?? new HeroSection();
        var existingAbout = await _context.AboutSections.OrderBy(x => x.Id).FirstOrDefaultAsync() ?? new AboutSection();
        var existingContact = await _context.ContactSettings.OrderBy(x => x.Id).FirstOrDefaultAsync() ?? new ContactSettings();

        existingSite.CompanyName = model.SiteSettings.CompanyName.Trim();
        existingSite.LogoText = model.SiteSettings.LogoText.Trim();
        existingSite.LogoSubText = model.SiteSettings.LogoSubText.Trim();
        existingSite.Tagline = model.SiteSettings.Tagline.Trim();
        existingSite.PrimaryPhone = model.SiteSettings.PrimaryPhone.Trim();
        existingSite.Email = model.SiteSettings.Email.Trim();
        existingSite.Address = model.SiteSettings.Address.Trim();
        existingSite.FooterText = model.SiteSettings.FooterText.Trim();
        existingSite.CopyrightText = model.SiteSettings.CopyrightText.Trim();
        existingSite.MetaTitle = model.SiteSettings.MetaTitle.Trim();
        existingSite.MetaDescription = model.SiteSettings.MetaDescription.Trim();
        existingSite.MetaKeywords = model.SiteSettings.MetaKeywords.Trim();
        existingSite.OpenGraphTitle = model.SiteSettings.OpenGraphTitle.Trim();
        existingSite.OpenGraphDescription = model.SiteSettings.OpenGraphDescription.Trim();

        existingHero.Title = model.HeroSection.Title.Trim();
        existingHero.HighlightTitle = model.HeroSection.HighlightTitle.Trim();
        existingHero.Description = model.HeroSection.Description.Trim();
        existingHero.PrimaryButtonText = model.HeroSection.PrimaryButtonText.Trim();
        existingHero.PrimaryButtonUrl = model.HeroSection.PrimaryButtonUrl.Trim();
        existingHero.SecondaryButtonText = model.HeroSection.SecondaryButtonText.Trim();
        existingHero.SecondaryButtonUrl = model.HeroSection.SecondaryButtonUrl.Trim();

        existingAbout.Title = model.AboutSection.Title.Trim();
        existingAbout.Subtitle = model.AboutSection.Subtitle.Trim();
        existingAbout.Description = model.AboutSection.Description.Trim();
        existingAbout.MissionTitle = model.AboutSection.MissionTitle.Trim();
        existingAbout.MissionDescription = model.AboutSection.MissionDescription.Trim();
        existingAbout.VisionTitle = model.AboutSection.VisionTitle.Trim();
        existingAbout.VisionDescription = model.AboutSection.VisionDescription.Trim();
        existingAbout.ValuesTitle = model.AboutSection.ValuesTitle.Trim();
        existingAbout.ValuesDescription = model.AboutSection.ValuesDescription.Trim();
        existingAbout.IsActive = model.AboutSection.IsActive;

        existingContact.CompanyName = model.ContactSettings.CompanyName.Trim();
        existingContact.Address = model.ContactSettings.Address.Trim();
        existingContact.Phone = model.ContactSettings.Phone.Trim();
        existingContact.Email = model.ContactSettings.Email.Trim();
        existingContact.BusinessHours = model.ContactSettings.BusinessHours.Trim();
        existingContact.MapUrl = model.ContactSettings.MapUrl.Trim();
        existingContact.FacebookUrl = model.ContactSettings.FacebookUrl?.Trim() ?? string.Empty;
        existingContact.InstagramUrl = model.ContactSettings.InstagramUrl?.Trim() ?? string.Empty;
        existingContact.LinkedInUrl = model.ContactSettings.LinkedInUrl?.Trim() ?? string.Empty;

        try
        {
            var logo = await _files.SaveImageAsync(logoFile);
            var hero = await _files.SaveImageAsync(heroFile);
            var about = await _files.SaveImageAsync(aboutFile);
            var og = await _files.SaveImageAsync(ogFile);

            if (!string.IsNullOrWhiteSpace(logo))
            {
                _files.DeleteImage(existingSite.LogoImageUrl);
                existingSite.LogoImageUrl = logo;
            }

            if (!string.IsNullOrWhiteSpace(hero))
            {
                _files.DeleteImage(existingHero.BackgroundImageUrl);
                existingHero.BackgroundImageUrl = hero;
            }
            else if (!string.IsNullOrWhiteSpace(model.HeroSection.BackgroundImageUrl))
            {
                existingHero.BackgroundImageUrl = model.HeroSection.BackgroundImageUrl;
            }

            if (!string.IsNullOrWhiteSpace(about))
            {
                _files.DeleteImage(existingAbout.ImageUrl);
                existingAbout.ImageUrl = about;
            }
            else if (!string.IsNullOrWhiteSpace(model.AboutSection.ImageUrl))
            {
                existingAbout.ImageUrl = model.AboutSection.ImageUrl;
            }

            if (!string.IsNullOrWhiteSpace(og))
            {
                _files.DeleteImage(existingSite.OpenGraphImageUrl);
                existingSite.OpenGraphImageUrl = og;
            }
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
