using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truck_company.Data;

namespace Truck_company.Areas.Admin.Controllers;

[Area("Admin"), Authorize(Roles = "Admin")]
public class ContactMessagesController : Controller
{
    private readonly ApplicationDbContext _db;
    public ContactMessagesController(ApplicationDbContext db) => _db = db;
    public async Task<IActionResult> Index(string? search)
    {
        var q = _db.ContactMessages.AsQueryable();
        if (!string.IsNullOrWhiteSpace(search)) q = q.Where(x => x.Name.Contains(search) || x.Email.Contains(search) || x.Message.Contains(search));
        return View(await q.OrderByDescending(x => x.CreatedAt).ToListAsync());
    }
    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id) { var item = await _db.ContactMessages.FindAsync(id); if (item is null) return NotFound(); _db.Remove(item); await _db.SaveChangesAsync(); return RedirectToAction(nameof(Index)); }
}
