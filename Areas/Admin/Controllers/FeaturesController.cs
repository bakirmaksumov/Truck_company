using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truck_company.Data;
using Truck_company.Models;
using Truck_company.ViewModels;

namespace Truck_company.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class FeaturesController : Controller
{
    private readonly ApplicationDbContext _db;

    public FeaturesController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _db.Features.OrderBy(x => x.DisplayOrder).ToListAsync());
    }

    public IActionResult Create()
    {
        return View("Edit", new FeatureInputViewModel());
    }

    public async Task<IActionResult> Edit(int id)
    {
        var entity = await _db.Features.FindAsync(id);
        if (entity is null) return NotFound();

        return View(new FeatureInputViewModel
        {
            Id = entity.Id,
            Title = entity.Title,
            Description = entity.Description,
            Icon = entity.Icon,
            DisplayOrder = entity.DisplayOrder,
            IsActive = entity.IsActive
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(FeatureInputViewModel input)
    {
        if (!ModelState.IsValid) return View("Edit", input);

        var entity = input.Id == 0 ? new Feature() : await _db.Features.FindAsync(input.Id);
        if (entity is null) return NotFound();

        entity.Title = input.Title.Trim();
        entity.Description = input.Description.Trim();
        entity.Icon = input.Icon;
        entity.DisplayOrder = input.DisplayOrder;
        entity.IsActive = input.IsActive;

        if (entity.Id == 0) _db.Features.Add(entity);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Features.FindAsync(id);
        if (item is null) return NotFound();

        _db.Features.Remove(item);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
