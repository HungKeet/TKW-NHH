using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NguyenHuyHung_241230739.Models;

namespace NguyenHuyHung_241230739.Controllers;

public class TaiSanController : Controller
{
    private readonly QltaiSanContext _context;

    public TaiSanController(QltaiSanContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        ViewBag.MaLoai = new SelectList(_context.LoaiTaiSans, "MaLoai", "TenLoai");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TaiSan taiSan)
    {
        if (ModelState.IsValid)
        {
            _context.TaiSans.Add(taiSan);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        ViewBag.MaLoai = new SelectList(_context.LoaiTaiSans, "MaLoai", "TenLoai", taiSan.MaLoai);
        return View(taiSan);
    }
}
