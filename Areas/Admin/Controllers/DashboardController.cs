using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Truck_company.Data;
using Truck_company.Models;
using Truck_company.ViewModels;

namespace Truck_company.Areas.Admin.Controllers;

[Area("Admin")]
[Authorize(Roles = "Admin")]
public class DashboardController : Controller
{
    private readonly ApplicationDbContext _context;

    public DashboardController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var model = new DashboardViewModel
        {
            TotalServices = await _context.Services.CountAsync(),
            ActiveServices = await _context.Services.CountAsync(x => x.IsActive),
            NewQuoteRequests = await _context.QuoteRequests.CountAsync(q => q.Status == QuoteStatus.New),
            TotalQuoteRequests = await _context.QuoteRequests.CountAsync(),
            TotalMessages = await _context.ContactMessages.CountAsync(),
            ActiveFeatures = await _context.Features.CountAsync(f => f.IsActive),
            RecentQuoteRequests = await _context.QuoteRequests
                .OrderByDescending(q => q.CreatedAt)
                .Take(5)
                .ToListAsync(),
            RecentContactMessages = await _context.ContactMessages
                .OrderByDescending(m => m.CreatedAt)
                .Take(5)
                .ToListAsync()
        };

        return View(model);
    }
}
