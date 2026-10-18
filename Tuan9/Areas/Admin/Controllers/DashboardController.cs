using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tuan9.Models;

namespace Tuan9.Areas.Admin.Controllers;

[Area("Admin")]
public class DashboardController : Controller
{
    private readonly AppDbContext _context;

    public DashboardController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.CategoryCount = await _context.Categories.CountAsync();
        ViewBag.ProductCount = await _context.Products.CountAsync();
        ViewBag.BannerCount = await _context.Banners.CountAsync();
        ViewBag.BlogCount = await _context.Blogs.CountAsync();

        ViewBag.RecentProducts = await _context.Products
            .Include(p => p.Category)
            .OrderByDescending(p => p.Id)
            .Take(5)
            .ToListAsync();

        ViewBag.RecentCategories = await _context.Categories
            .Include(c => c.Products)
            .OrderByDescending(c => c.Id)
            .Take(5)
            .ToListAsync();

        return View();
    }
}
