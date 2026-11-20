using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NguyenHuyHung_241230739.Models;

namespace NguyenHuyHung_241230739.Controllers;

public class SachController : Controller
{
    private  readonly QlnhaSachContext _context;
    public SachController(QlnhaSachContext context)
    {
        _context = context;
    }
    public IActionResult Create()
    {
        ViewBag.MaTl = new SelectList(_context.TheLoais, "MaTl" , "TenTl" );
        return View();
    }
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Sach sach)
    {
        ModelState.Remove("MaTlNavigation");
        if (ModelState.IsValid)
        {
            _context.Saches.Add(sach);
            _context.SaveChanges();
            return RedirectToAction("Index", "Home");

        }
        ViewBag.Matl = new SelectList(_context.TheLoais, "MaTl", "TenTl", sach.MaTl);
        return View(sach);
    }
}