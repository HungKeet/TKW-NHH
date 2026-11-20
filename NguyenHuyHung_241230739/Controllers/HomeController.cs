using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using NguyenHuyHung_241230739.Models;

namespace NguyenHuyHung_241230739.Controllers;

public class HomeController : Controller
{
    private readonly QlnhaSachContext _context;

    public HomeController(QlnhaSachContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var dsSach = _context.Saches
            .Where(s => s.Gia != null && s.Gia >= 100)
            .ToList();
        return View(dsSach);
    }

    public IActionResult SachTheoTheLoai(int? maTL)
    {
        if (maTL == null || maTL == 0)
        {
            var tatCaSach = _context.Saches
                .Where(s => s.Gia != null && s.Gia >= 100)
                .ToList();
            return PartialView("_DanhSachSachPartial", tatCaSach);
        }

        var dsTheoLoai = _context.Saches
            .Where(s => s.MaTl == maTL && s.Gia != null)
            .ToList();
        return PartialView("_DanhSachSachPartial", dsTheoLoai);
    }
}