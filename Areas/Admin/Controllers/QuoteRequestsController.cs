using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truck_company.Data;
using Truck_company.Models;

namespace Truck_company.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class QuoteRequestsController : Controller
{
    private readonly ApplicationDbContext _context;

    public QuoteRequestsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string? status, string? search)
    {
        var requests = _context.QuoteRequests.AsQueryable();

        if (!string.IsNullOrWhiteSpace(status) && Enum.TryParse<QuoteStatus>(status, true, out var parsedStatus))
        {
            requests = requests.Where(q => q.Status == parsedStatus);
        }

        if (!string.IsNullOrWhiteSpace(search))
        {
            var term = search.Trim();
            requests = requests.Where(q => q.FullName.Contains(term) || q.CompanyName.Contains(term) || q.Email.Contains(term) || q.Phone.Contains(term));
        }

        var model = await requests.OrderByDescending(q => q.CreatedAt).ToListAsync();
        ViewBag.SelectedStatus = status ?? "All";
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var request = await _context.QuoteRequests.FindAsync(id);
        if (request == null)
        {
            return NotFound();
        }

        return View(request);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> UpdateStatus(int id, QuoteStatus status)
    {
        var request = await _context.QuoteRequests.FindAsync(id);
        if (request == null)
        {
            return NotFound();
        }

        request.Status = status;
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        var request = await _context.QuoteRequests.FindAsync(id);
        if (request == null)
        {
            return NotFound();
        }

        _context.QuoteRequests.Remove(request);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}
