using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truck_company.Data;
using Truck_company.Models;

namespace Truck_company.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin")]
public class StatisticsController : Controller
{
    private readonly ApplicationDbContext _db;
    public StatisticsController(ApplicationDbContext db) => _db = db;
    public async Task<IActionResult> Index() => View(await _db.Statistics.OrderBy(x => x.DisplayOrder).ToListAsync());
    public IActionResult Create() => View("Edit", new Statistic());
    public async Task<IActionResult> Edit(int id) => View(await _db.Statistics.FindAsync(id) ?? new Statistic());
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(Statistic input)
    {
        if (string.IsNullOrWhiteSpace(input.Label) || string.IsNullOrWhiteSpace(input.Value)) ModelState.AddModelError(string.Empty, "Value and label are required.");
        if (!ModelState.IsValid) return View("Edit", input);
        var item = input.Id == 0 ? new Statistic() : await _db.Statistics.FindAsync(input.Id);
        if (item is null) return NotFound();
        item.Value = input.Value; item.Label = input.Label; item.Icon = input.Icon; item.DisplayOrder = input.DisplayOrder; item.IsActive = input.IsActive;
        if (item.Id == 0) _db.Statistics.Add(item); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index));
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id) { var item = await _db.Statistics.FindAsync(id); if (item is null) return NotFound(); _db.Remove(item); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
}
