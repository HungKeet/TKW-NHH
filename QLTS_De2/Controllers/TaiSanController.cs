using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenHuyHung_241230739.Models;

namespace NguyenHuyHung_241230739.Controllers;

public class TaiSanController : Controller
{
    private readonly QltaiSanContext _context;

    public TaiSanController(QltaiSanContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var taiSan = await _context.TaiSans.FindAsync(id);
        if (taiSan == null)
        {
            return NotFound();
        }

        ViewBag.MaLoai = new SelectList(_context.LoaiTaiSans, "MaLoai", "TenLoai", taiSan.MaLoai);
        return View(taiSan);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TaiSan taiSan)
    {
        if (id != taiSan.MaTs)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(taiSan);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.TaiSans.Any(e => e.MaTs == taiSan.MaTs))
                {
                    return NotFound();
                }
                throw;
            }
        }
        ViewBag.MaLoai = new SelectList(_context.LoaiTaiSans, "MaLoai", "TenLoai", taiSan.MaLoai);
        return View(taiSan);
    }
}
