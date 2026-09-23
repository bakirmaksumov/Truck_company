using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truck_company.Data;
using Truck_company.Models;
using Truck_company.ViewModels;

namespace Truck_company.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class StatisticsController : Controller
{
    private readonly ApplicationDbContext _db;

    public StatisticsController(ApplicationDbContext db)
    {
        _db = db;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _db.Statistics.OrderBy(x => x.DisplayOrder).ToListAsync());
    }

    public IActionResult Create()
    {
        return View("Edit", new StatisticInputViewModel());
    }

    public async Task<IActionResult> Edit(int id)
    {
        var entity = await _db.Statistics.FindAsync(id);
        if (entity is null) return NotFound();

        return View(new StatisticInputViewModel
        {
            Id = entity.Id,
            Value = entity.Value,
            Label = entity.Label,
            Icon = entity.Icon,
            DisplayOrder = entity.DisplayOrder,
            IsActive = entity.IsActive
        });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Save(StatisticInputViewModel input)
    {
        if (!ModelState.IsValid) return View("Edit", input);

        var entity = input.Id == 0 ? new Statistic() : await _db.Statistics.FindAsync(input.Id);
        if (entity is null) return NotFound();

        entity.Value = input.Value.Trim();
        entity.Label = input.Label.Trim();
        entity.Icon = input.Icon;
        entity.DisplayOrder = input.DisplayOrder;
        entity.IsActive = input.IsActive;

        if (entity.Id == 0) _db.Statistics.Add(entity);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _db.Statistics.FindAsync(id);
        if (item is null) return NotFound();

        _db.Statistics.Remove(item);
        await _db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
