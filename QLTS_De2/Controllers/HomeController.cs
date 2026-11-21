using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenHuyHung_241230739.Models;

namespace NguyenHuyHung_241230739.Controllers;

public class HomeController : Controller
{
    private readonly QltaiSanContext _context;

    public HomeController(QltaiSanContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var dsTaiSan = await _context.TaiSans
            .Where(ts => ts.NguyenGia != null && ts.NguyenGia >= 20)
            .ToListAsync();
        return View(dsTaiSan);
    }

    public async Task<IActionResult> TaiSanTheoLoai(int? maLoai)
    {
        var query = _context.TaiSans.Where(ts => ts.NguyenGia != null && ts.NguyenGia >= 20);
        if (maLoai.HasValue && maLoai.Value > 0)
        {
            query = query.Where(ts => ts.MaLoai == maLoai.Value);
        }
        var dsTaiSan = await query.ToListAsync();
        return PartialView("_DanhSachTaiSanPartial", dsTaiSan);
    }
}
