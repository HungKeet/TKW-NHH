using Microsoft.AspNetCore.Mvc;
using NguyenHuyHung_241230739.Models;

namespace NguyenHuyHung_241230739.ViewComponents;

public class LoaiHangMenuViewComponent : ViewComponent
{
    private readonly QlhangHoaContext _context;

    public LoaiHangMenuViewComponent(QlhangHoaContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var loaiHangs = _context.LoaiHangs.ToList();
        return View(loaiHangs);
    }
}