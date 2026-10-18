using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tuan9.Models;

namespace Tuan9.Controllers;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var model = new HomeViewModel
        {
            Banners = await _context.Banners.Where(b => b.Status == 1).OrderBy(b => b.Prioty).ToListAsync(),
            Categories = await _context.Categories.Where(c => c.Status == 1).ToListAsync(),
            Products = await _context.Products.Include(p => p.Category).Where(p => p.Status == 1).ToListAsync(),
            Blogs = await _context.Blogs.Where(b => b.Status == 1).OrderByDescending(b => b.CreatedDate).ToListAsync()
        };

        return View(model);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
