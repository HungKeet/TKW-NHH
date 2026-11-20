using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NguyenHuyHung_241230739.Models;

namespace NguyenHuyHung_241230739.Controllers;

public class HangHoaController : Controller
{
    private readonly QlhangHoaContext _context;

    public HangHoaController(QlhangHoaContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        ViewBag.MaLoai = new SelectList(_context.LoaiHangs, "MaLoai", "TenLoai");
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HangHoa hangHoa)
    {
        if (ModelState.IsValid)
        {
            _context.HangHoas.Add(hangHoa);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home");
        }
        ViewBag.MaLoai = new SelectList(_context.LoaiHangs, "MaLoai", "TenLoai", hangHoa.MaLoai);
        return View(hangHoa);
    }
}