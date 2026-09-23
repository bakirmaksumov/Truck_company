using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truck_company.Data;
using Truck_company.Models;

namespace Truck_company.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin")]
public class FeaturesController : Controller
{
    private readonly ApplicationDbContext _db;
    public FeaturesController(ApplicationDbContext db) => _db = db;
    public async Task<IActionResult> Index() => View(await _db.Features.OrderBy(x => x.DisplayOrder).ToListAsync());
    public IActionResult Create() => View("Edit", new Feature());
    public async Task<IActionResult> Edit(int id) => View(await _db.Features.FindAsync(id) ?? new Feature());
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(Feature input)
    {
        if (string.IsNullOrWhiteSpace(input.Title)) ModelState.AddModelError(nameof(input.Title), "Title is required.");
        if (!ModelState.IsValid) return View("Edit", input);
        var item = input.Id == 0 ? new Feature() : await _db.Features.FindAsync(input.Id);
        if (item is null) return NotFound();
        item.Title = input.Title.Trim(); item.Description = input.Description; item.Icon = input.Icon;
        item.DisplayOrder = input.DisplayOrder; item.IsActive = input.IsActive;
        if (item.Id == 0) _db.Features.Add(item);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Features.FindAsync(id); if (item is null) return NotFound();
        _db.Features.Remove(item); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index));
    }
}
