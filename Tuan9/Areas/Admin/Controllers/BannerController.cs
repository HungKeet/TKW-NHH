using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tuan9.Models;

namespace Tuan9.Areas.Admin.Controllers;

[Area("Admin")]
public class BannerController : Controller
{
    private readonly AppDbContext _context;

    public BannerController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string search)
    {
        var query = _context.Banners.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(b => b.Name.Contains(search));
            ViewBag.Search = search;
        }

        var banners = await query.OrderBy(b => b.Prioty).ToListAsync();
        return View(banners);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var banner = await _context.Banners.FirstOrDefaultAsync(m => m.Id == id);
        if (banner == null)
        {
            return NotFound();
        }

        return View(banner);
    }

    public IActionResult Create()
    {
        return View(new Banner());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Banner banner)
    {
        if (await _context.Banners.AnyAsync(b => b.Name.ToLower() == banner.Name.ToLower()))
        {
            ModelState.AddModelError("Name", "Tên banner này đã tồn tại.");
        }

        if (ModelState.IsValid)
        {
            _context.Add(banner);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm mới banner thành công!";
            return RedirectToAction(nameof(Index));
        }

        return View(banner);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var banner = await _context.Banners.FindAsync(id);
        if (banner == null)
        {
            return NotFound();
        }

        return View(banner);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Banner banner)
    {
        if (id != banner.Id)
        {
            return NotFound();
        }

        if (await _context.Banners.AnyAsync(b => b.Name.ToLower() == banner.Name.ToLower() && b.Id != id))
        {
            ModelState.AddModelError("Name", "Tên banner này đã tồn tại.");
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(banner);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật banner thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Banners.AnyAsync(e => e.Id == banner.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        return View(banner);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var banner = await _context.Banners.FirstOrDefaultAsync(m => m.Id == id);
        if (banner == null)
        {
            return NotFound();
        }

        return View(banner);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var banner = await _context.Banners.FindAsync(id);
        if (banner != null)
        {
            _context.Banners.Remove(banner);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Xóa banner thành công!";
        }

        return RedirectToAction(nameof(Index));
    }
}
