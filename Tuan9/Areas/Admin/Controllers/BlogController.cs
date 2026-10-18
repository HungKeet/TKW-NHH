using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tuan9.Models;

namespace Tuan9.Areas.Admin.Controllers;

[Area("Admin")]
public class BlogController : Controller
{
    private readonly AppDbContext _context;

    public BlogController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string search)
    {
        var query = _context.Blogs.AsQueryable();

        if (!string.IsNullOrEmpty(search))
        {
            query = query.Where(b => b.Name.Contains(search));
            ViewBag.Search = search;
        }

        var blogs = await query.OrderByDescending(b => b.Id).ToListAsync();
        return View(blogs);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var blog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);
        if (blog == null)
        {
            return NotFound();
        }

        return View(blog);
    }

    public IActionResult Create()
    {
        return View(new Blog());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Blog blog)
    {
        if (await _context.Blogs.AnyAsync(b => b.Name.ToLower() == blog.Name.ToLower()))
        {
            ModelState.AddModelError("Name", "Tiêu đề blog này đã tồn tại.");
        }

        if (ModelState.IsValid)
        {
            _context.Add(blog);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Thêm mới blog thành công!";
            return RedirectToAction(nameof(Index));
        }

        return View(blog);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var blog = await _context.Blogs.FindAsync(id);
        if (blog == null)
        {
            return NotFound();
        }

        return View(blog);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Blog blog)
    {
        if (id != blog.Id)
        {
            return NotFound();
        }

        if (await _context.Blogs.AnyAsync(b => b.Name.ToLower() == blog.Name.ToLower() && b.Id != id))
        {
            ModelState.AddModelError("Name", "Tiêu đề blog này đã tồn tại.");
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(blog);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật blog thành công!";
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await _context.Blogs.AnyAsync(e => e.Id == blog.Id))
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

        return View(blog);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var blog = await _context.Blogs.FirstOrDefaultAsync(m => m.Id == id);
        if (blog == null)
        {
            return NotFound();
        }

        return View(blog);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var blog = await _context.Blogs.FindAsync(id);
        if (blog != null)
        {
            _context.Blogs.Remove(blog);
            await _context.SaveChangesAsync();
            TempData["SuccessMessage"] = "Xóa blog thành công!";
        }

        return RedirectToAction(nameof(Index));
    }
}
