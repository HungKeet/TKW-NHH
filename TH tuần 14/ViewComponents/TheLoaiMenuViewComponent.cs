using Microsoft.AspNetCore.Mvc;
using NguyenHuyHung_241230739.Models;

namespace NguyenHuyHung_241230739.ViewComponents;

public class TheLoaiMenuViewComponent : ViewComponent
{
    private readonly QlnhaSachContext _context;

    public TheLoaiMenuViewComponent(QlnhaSachContext context)
    {
        _context = context;
    }

    public IViewComponentResult Invoke()
    {
        var theLoais = _context.TheLoais.ToList();
        return View(theLoais);
    }
}
