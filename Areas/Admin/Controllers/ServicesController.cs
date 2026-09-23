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
public class ServicesController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly IFileService _files;

    public ServicesController(ApplicationDbContext context, IFileService files)
    {
        _context = context;
        _files = files;
    }

    public async Task<IActionResult> Index()
    {
        var services = await _context.Services.OrderBy(x => x.DisplayOrder).ToListAsync();
        return View(services);
    }

    public IActionResult Create()
    {
        return View(new ServiceInputViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ServiceInputViewModel model, IFormFile? imageFile)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var entity = new Service
        {
            Title = model.Title.Trim(),
            ShortDescription = model.ShortDescription.Trim(),
            FullDescription = model.FullDescription?.Trim() ?? string.Empty,
            Icon = model.Icon,
            DisplayOrder = model.DisplayOrder,
            IsActive = model.IsActive,
            ImageUrl = model.ImageUrl ?? string.Empty
        };

        try
        {
            var uploaded = await _files.SaveImageAsync(imageFile);
            if (!string.IsNullOrWhiteSpace(uploaded)) entity.ImageUrl = uploaded;
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(nameof(model.ImageUrl), ex.Message);
            return View(model);
        }

        _context.Services.Add(entity);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service == null)
        {
            return NotFound();
        }

        return View(new ServiceInputViewModel
        {
            Id = service.Id,
            Title = service.Title,
            ShortDescription = service.ShortDescription,
            FullDescription = service.FullDescription,
            Icon = service.Icon,
            ImageUrl = service.ImageUrl,
            DisplayOrder = service.DisplayOrder,
            IsActive = service.IsActive
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, ServiceInputViewModel model, IFormFile? imageFile)
    {
        if (id != model.Id)
        {
            return BadRequest();
        }

        if (!ModelState.IsValid)
        {
            return View(model);
        }

        var existing = await _context.Services.FindAsync(id);
        if (existing is null)
        {
            return NotFound();
        }

        existing.Title = model.Title.Trim();
        existing.ShortDescription = model.ShortDescription.Trim();
        existing.FullDescription = model.FullDescription?.Trim() ?? string.Empty;
        existing.Icon = model.Icon;
        existing.DisplayOrder = model.DisplayOrder;
        existing.IsActive = model.IsActive;

        try
        {
            var uploaded = await _files.SaveImageAsync(imageFile);
            if (!string.IsNullOrWhiteSpace(uploaded))
            {
                _files.DeleteImage(existing.ImageUrl);
                existing.ImageUrl = uploaded;
            }
            else if (!string.IsNullOrWhiteSpace(model.ImageUrl))
            {
                existing.ImageUrl = model.ImageUrl;
            }
        }
        catch (InvalidOperationException ex)
        {
            ModelState.AddModelError(nameof(model.ImageUrl), ex.Message);
            return View(model);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service == null)
        {
            return NotFound();
        }

        _files.DeleteImage(service.ImageUrl);
        _context.Services.Remove(service);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
