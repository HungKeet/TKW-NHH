using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenHuyHung_241230739.Models;

namespace NguyenHuyHung_241230739.Controllers;

public class HomeController : Controller
{
    private readonly QlhangHoaContext _context;

    public HomeController(QlhangHoaContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var dsHangHoa = await _context.HangHoas
            .Where(h => h.Gia >= 100)
            .ToListAsync();
        return View(dsHangHoa);
    }

    public async Task<IActionResult> HangHoaTheoLoai(int? maLoai)
    {
        var query = _context.HangHoas.Where(h => h.Gia >= 100);
        if (maLoai.HasValue && maLoai.Value > 0)
        {
            query = query.Where(h => h.MaLoai == maLoai.Value);
        }
        var dsHangHoa = await query.ToListAsync();
        return PartialView("_DanhSachHangHoaPartial", dsHangHoa);
    }
}