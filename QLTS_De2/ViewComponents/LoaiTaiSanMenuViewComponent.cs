using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NguyenHuyHung_241230739.Models;

namespace NguyenHuyHung_241230739.ViewComponents;

public class LoaiTaiSanMenuViewComponent : ViewComponent
{
    private readonly QltaiSanContext _context;

    public LoaiTaiSanMenuViewComponent(QltaiSanContext context)
    {
        _context = context;
    }

    public async Task<IViewComponentResult> InvokeAsync()
    {
        var loais = await _context.LoaiTaiSans.ToListAsync();
        return View(loais);
    }
}
